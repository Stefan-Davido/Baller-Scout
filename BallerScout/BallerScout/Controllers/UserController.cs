using AutoMapper;
using BallerScout.Data;
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
    public class UserController : Controller
    {
        private readonly IPostService _postService;
        private readonly IFollowingService _followingService;
        private readonly ISkillsService _skillsService;
        private readonly IGameService _gameService;
        private readonly IPlayerHistoryService _playerHistoryService;
        private readonly ISeasonService _seasonService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPostService postService,
            IFollowingService followingService,
            ISkillsService skillsService,
            IGameService gameService,
            IPlayerHistoryService playerHistoryService,
            ISeasonService seasonService,
            IMapper mapper,
            DataContext dataContext)
        {
            _postService = postService;
            _userManager = userManager;
            _signInManager = signInManager;
            _followingService = followingService;
            _skillsService = skillsService;
            _gameService = gameService;
            _seasonService = seasonService;
            _playerHistoryService = playerHistoryService;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var allPosts = _postService.GetListOfPostsByUserId(Id).OrderByDescending(x => x.DatePosted);
            var numberOfFollowers = _followingService.NumberOfFollowers(Id);
            var numberOfFollowings = _followingService.NumberOfFollowings(Id);
            var skills = _skillsService.GetSkillsByUserId(Id);

            ApplicationUserModel userModel = new ApplicationUserModel();
            userModel = _mapper.Map<ApplicationUser, ApplicationUserModel>(user);

            userModel.NumberOfFollowings = numberOfFollowings;
            userModel.NumberOfFollowers = numberOfFollowers;          
            userModel.DateCreated = DateTime.Now;
            userModel.Posts = allPosts;
            userModel.NumberOfPosts = allPosts.Count();
            userModel.Skills = skills;

            return View(userModel);
        }
        public async Task FollowAndUnfollow(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var signInUser = await _userManager.GetUserAsync(User);
            var signInUserId = await _userManager.GetUserIdAsync(signInUser);
            var followCheck = _followingService.FollowCheck(signInUserId, Id);

            if (followCheck == true)
            {
                _followingService.Unfollow(signInUser.Id, Id);
                signInUser.NumberOfFollowings -= 1;
                user.NumberOfFollowers -= 1;
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateAsync(signInUser);
                await _dataContext.SaveChangesAsync();
            }
            else
            {  
                Following newFollowingList = new Following()
                {
                    UserId = signInUserId,
                    UserName = signInUser.FirstName,
                    UserIFollowId = user.Id,
                    UserIFollowName = user.FirstName,
                    FollowingBool = true,
                    DateCreated = DateTime.Now
                };

                _followingService.AddFollowingList(newFollowingList);
                signInUser.NumberOfFollowings += 1;
                user.NumberOfFollowers += 1;
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateAsync(signInUser);
                await _dataContext.SaveChangesAsync();
            }
        }

        [HttpGet]
        public IActionResult AllUserPosts(string Id)
        {
            var userPosts = _postService.GetListOfPostsByUserId(Id).OrderByDescending(x => x.DatePosted).Reverse();
            return View(userPosts);
        }

        public async Task<IActionResult> AllFollowings(string Id)
        {
            var followingList = _followingService.GetFollowingListByUserId(Id);
            List<ApplicationUserModel> models = new List<ApplicationUserModel>();

            foreach (var followingUser in followingList)
            {
                ApplicationUserModel userModel = new ApplicationUserModel();
                var user = await _userManager.FindByIdAsync(followingUser.UserIFollowId);
                userModel = _mapper.Map<ApplicationUser, ApplicationUserModel>(user);
                
                models.Add(userModel);
            }

            if (models.Count() == 0)
            {
                return RedirectToAction(nameof(ZeroFollowers));
            }
            else
            {
                return View(models.AsEnumerable());
            }
        }

        public async Task<IActionResult> UserExperience(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var experienceModel = new UserExperienceModel();
            experienceModel.UserId = id;
            experienceModel.UserFullName = user.FirstName + " " + user.LastName;
            experienceModel.UserGames = _gameService.GetGamesByUserId(id);
            experienceModel.UserTransfers = _playerHistoryService.GetPlayerHistoriesByUserId(id);
            experienceModel.UserSeasons = _seasonService.GetSeasonsByUserId(id);

            return View(experienceModel);
        }

        public async Task<IActionResult> AllFollowers(string Id)
        {
            var allFollowers = await _followingService.UsersWhoFollowMe(Id);
            List<ApplicationUserModel> models = new List<ApplicationUserModel>();

            foreach (var follower in allFollowers)
            {
                ApplicationUserModel userModel = new ApplicationUserModel();
                userModel = _mapper.Map<ApplicationUser, ApplicationUserModel>(follower);

                models.Add(userModel);
            }

            if (models.Count() == 0)
            {
                return RedirectToAction(nameof(ZeroFollowers));
            }
            else
            {
                return View(models.AsEnumerable());
            }
        }

        public IActionResult ZeroFollowers()
        {
            return View();
        }
    }
}
