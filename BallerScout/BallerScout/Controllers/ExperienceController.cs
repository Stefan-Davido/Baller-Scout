using BallerScout.Entities;
using BallerScout.Models;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPlayerHistoryService _playerHistoryService;
        private readonly ISeasonService _seasonService;
        private readonly IGameService _gameService;
        private readonly IDateConverterService _dateConverterService;
        private readonly IDropDownsService _dropDownsService;
        private readonly IPostService _postService;

        public ExperienceController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPlayerHistoryService playerHistoryService,
            ISeasonService seasonService,
            IGameService gameService,
            IDateConverterService dateConverterService,
            IDropDownsService dropDownsService,
            IPostService postService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _playerHistoryService = playerHistoryService;
            _seasonService = seasonService;
            _gameService = gameService;
            _dateConverterService = dateConverterService;
            _dropDownsService = dropDownsService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddExperience()
        {
            var user = await _userManager.GetUserAsync(User);
            var clubName = _playerHistoryService.GetCurrentClubByUserId(user.Id);
            ViewBag.ClubName = clubName;
            var position = await _dropDownsService.Postition(user.Id);
            ViewBag.Positions = position;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExperience(Game game)
        {
            var user = await _userManager.GetUserAsync(User);

            game.UserId = user.Id;
            game.UserFullName = user.FirstName + " " + user.LastName;
            game.Club = _playerHistoryService.GetCurrentClubByUserId(user.Id);
            game.SeasonYearString = SeasonConverter(game.DatePlayed);
            game.DatePlayedString = game.DatePlayed.ToShortDateString();
            game.SeasonYear = DateTime.Parse(String.Format("{0:1 1 yyyy}", game.DatePlayed));

            var gameDate = game.DatePlayed;
            var afterAugust = _dateConverterService.NeedNewSeason(gameDate);

            if (afterAugust == false)
            {
                var seasonId = _dateConverterService.SeasonOneYearMinusCheck(gameDate, game.UserId);

                await CheckSeasonById(seasonId, game);
            }
            else
            {
                var seasonId = _dateConverterService.SeasonOneYearPlusCheck(gameDate, game.UserId);

                await CheckSeasonById(seasonId, game);
            }

            _gameService.AddGame(game);

            return RedirectToAction(nameof(Index));         
        }

        [HttpGet]
        public IActionResult EditExperience(int id)
        {
            Game game = _gameService.GetGameById(id);

            GameModel gameModel = new GameModel();
            gameModel.GameId = game.GameId;
            gameModel.UserId = game.UserId;
            gameModel.SeasonId = game.SeasonId;
            gameModel.UserFullName = game.UserFullName;
            gameModel.Club = game.Club;
            gameModel.VersusClub = game.VersusClub;
            gameModel.Assists = game.Assists;
            gameModel.GoalsScored = game.GoalsScored;
            gameModel.RedCards = game.RedCards;
            gameModel.YellowCards = game.YellowCards;
            gameModel.SeasonYear = game.SeasonYear;
            gameModel.SeasonId = game.SeasonId;
            gameModel.SeasonYearString = game.SeasonYearString;
            gameModel.PlayedMinutes = game.PlayedMinutes;
            gameModel.DatePlayed = game.DatePlayed;
            gameModel.DatePlayedString = game.DatePlayedString;
            gameModel.GoalsScoredDecrementInSeason = game.GoalsScored;
            gameModel.AssistsDecrementInSeason = game.Assists;
            gameModel.YellowCardsDecrementInSeason = game.YellowCards;
            gameModel.RedCardsDecrementInSeason = game.RedCards;

            if (game.PlayedMinutes > 0) 
            {
                gameModel.MinutesPlayedDecrementInSeason = 1;
            }
            else
            {
                gameModel.MinutesPlayedDecrementInSeason = 0;
            }

            var clubName = _playerHistoryService.GetCurrentClubByUserId(game.UserId);
            ViewBag.ClubName = clubName;

            return View(gameModel);
        }

        [HttpPost]
        public IActionResult EditExperience(GameModel gameModel)
        {
            Game game = _gameService.GetGameById(gameModel.GameId);
            Season season = _seasonService.GetSeasonById(gameModel.SeasonId);

            game.GameId = gameModel.GameId;
            game.UserId = gameModel.UserId;
            game.UserFullName = gameModel.UserFullName;
            game.Club = gameModel.Club;
            game.VersusClub = gameModel.VersusClub;
            game.Assists = gameModel.Assists;
            game.GoalsScored = gameModel.GoalsScored;
            game.RedCards = gameModel.RedCards;
            game.YellowCards = gameModel.YellowCards;
            game.SeasonYear = gameModel.SeasonYear;
            game.SeasonId = gameModel.SeasonId;
            game.SeasonYearString = gameModel.SeasonYearString;
            game.PlayedMinutes = gameModel.PlayedMinutes;
            game.DatePlayed = gameModel.DatePlayed;
            game.DatePlayedString = gameModel.DatePlayedString;

            UpdateSeasonWithEditedGame(season, gameModel);

            _gameService.UpdateGame(game);
            _seasonService.UpdateSeason(season);

            return RedirectToAction(nameof(Index));          
        }
            

        [HttpGet]
        public IActionResult DeleteExperience(int id)
        {
            var game = _gameService.GetGameById(id);
            return View(game);
        }
        
        [HttpPost, ActionName("DeleteExperience")]
        public IActionResult DeleteGame(int id)
        {
            Game game = _gameService.GetGameById(id);
            Season season =_seasonService.GetSeasonById(game.SeasonId);

            season.GamesPlayed -= 1;
            season.GoalsScored -= game.GoalsScored;
            season.Assists -= game.Assists;
            season.YellowCards -= game.YellowCards;
            season.RedCards -= game.RedCards;

            if(season.GamesPlayed == 0 &&
                season.Assists == 0 &&
                season.GoalsScored == 0 &&
                season.RedCards == 0 &&
                season.RedCards == 0)
            {
                _seasonService.DeleteSeason(season.SeasonId);
            }

            _gameService.DeleteGame(game.GameId);
            _seasonService.UpdateSeason(season);

            return View(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddTransfer()
        {
            var user = await _userManager.GetUserAsync(User);
            var clubName = _playerHistoryService.GetCurrentClubByUserId(user.Id);
            ViewBag.ClubName = clubName;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTransfer(PlayerHistory playerHistory)
        {
            var user = await _userManager.GetUserAsync(User);
            var clubName = _playerHistoryService.GetCurrentClubByUserId(user.Id);
            playerHistory.FromClub = clubName;

            playerHistory.UserId = user.Id;
            playerHistory.UserFullName = user.FirstName + " " + user.LastName;
            playerHistory.TransferDateString = playerHistory.TransferDate.ToShortDateString();

            _playerHistoryService.AddPlayerHistory(playerHistory);

            var post = new Post();
            post.DatePosted = playerHistory.TransferDate;
            post.DatePostedString = playerHistory.TransferDate.ToShortDateString();
            post.Description =
            (
                $" {playerHistory.UserFullName} " +
                $" is making a history transfer from " +
                $" {playerHistory.FromClub} to {playerHistory.ToClub} "
            );
            post.UserId = playerHistory.UserId;
            post.UserName = playerHistory.UserFullName;
            post.UserProfilePhoto = user.ImgURL;
            post.PhotoUrl = null;

            _postService.AddPost(post);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditTransfer(int id)
        {
            var playerHistory = _playerHistoryService.GetPlayerHistoryById(id);
            return View(playerHistory);
        } 
        
        public IActionResult EditTransfer(PlayerHistory playerHistory)
        {
            _playerHistoryService.UpdatePlayerHistory(playerHistory);

            return RedirectToAction(nameof(Index));
        }

        #region Helper Method's

        private async Task CheckSeasonById(int seasonId, Game game)
        {
            if (seasonId > 0)
            {
                var season = _seasonService.GetSeasonById(seasonId);

                game.SeasonId = season.SeasonId;

                if (game.PlayedMinutes > 0)
                {
                    season.GamesPlayed += 1;
                }

                season.GoalsScored += game.GoalsScored;
                season.Assists += game.Assists;
                if (game.YellowCards == 2)
                {
                    game.RedCards = 1;
                    season.RedCards += 1;
                    season.YellowCards += 2;
                }
                else if (game.YellowCards == 1 && game.RedCards == 1)
                {
                    game.YellowCards = 2;
                    game.RedCards = 1;
                    season.RedCards += 1;
                    season.YellowCards += 2;
                }
                else
                {
                    season.RedCards += game.RedCards;
                    season.YellowCards += game.YellowCards;
                }
            }
            else
            {
                await AddSeason(game);
            }
        }
        private async Task AddSeason(Game game)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            Season season = new Season();
            season.UserId = userId;
            season.SeasonYearString = SeasonConverter(game.SeasonYear);
            season.SeasonYear = game.SeasonYear;
            season.UserFullName = user.FirstName + " " + user.LastName;
            season.Club = game.Club;
            season.SeasonYear = game.SeasonYear;
            season.GoalsScored = game.GoalsScored;
            season.Assists = game.Assists;
            if(game.YellowCards == 2)
            {
                game.RedCards = 1;
                season.RedCards = 1;
                season.YellowCards = 2;
            }
            else if(game.YellowCards == 1 && game.RedCards == 1)
            {
                game.YellowCards = 2;
                season.YellowCards = 2;
                game.RedCards = 1;
                season.RedCards = 1;
            }
            else
            {
                season.RedCards = game.RedCards;
                season.YellowCards = game.YellowCards;
            }

            if (game.PlayedMinutes > 0)
            {
                season.GamesPlayed = 1;
            }
            else
            {
                season.GamesPlayed = 0;
            }

            _seasonService.AddSeason(season);
            game.SeasonId = season.SeasonId;
        }

        private void UpdateSeasonWithEditedGame(Season season, GameModel gameModel)
        {
            if (gameModel.MinutesPlayedDecrementInSeason > 0)
            {
                season.GamesPlayed -= 1;
            }

            if (gameModel.AssistsDecrementInSeason > 0)
            {
                season.Assists -= gameModel.AssistsDecrementInSeason;
            }

            if (gameModel.GoalsScoredDecrementInSeason > 0)
            {
                season.GoalsScored -= gameModel.GoalsScoredDecrementInSeason;
            }

            if (gameModel.RedCardsDecrementInSeason > 0)
            {
                season.RedCards -= gameModel.RedCardsDecrementInSeason;
            }

            if (gameModel.YellowCardsDecrementInSeason > 0)
            {
                season.YellowCards -= gameModel.YellowCardsDecrementInSeason;
            }

            season.GamesPlayed += gameModel.PlayedMinutes;
            season.GoalsScored += gameModel.GoalsScored;
            season.Assists += gameModel.Assists;
            if (gameModel.YellowCards == 2)
            {
                season.RedCards = 1;
                season.YellowCards = 2;
            }
            else if (gameModel.YellowCards == 1 && gameModel.RedCards == 1)
            {
                season.YellowCards = 2;
                season.RedCards = 1;
            }
            else
            {
                season.RedCards += gameModel.RedCards;
                season.YellowCards += gameModel.YellowCards;
            }

            _seasonService.UpdateSeason(season);
        }

        private string SeasonConverter(DateTime datePlayed)
        {
            var needNewSeason = _dateConverterService.NeedNewSeason(datePlayed);
            string seasonYears = "";

            if(needNewSeason == true)
            {
                seasonYears = _dateConverterService.SeasonYearPlusOneConverter(datePlayed);
            }
            else
            {
                seasonYears = _dateConverterService.SeasonYearPlusOneConverter(datePlayed);
            }

            return seasonYears;
        }

        #endregion
    }
}
