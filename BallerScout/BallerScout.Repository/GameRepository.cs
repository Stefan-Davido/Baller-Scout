using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _dataContext;

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddGame(Game game)
        {
            _dataContext.Game.Add(game);
            _dataContext.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            _dataContext.Game.Update(game);
            _dataContext.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var game = GetGameById(id);
            _dataContext.Game.Remove(game);
            _dataContext.SaveChanges();
        }

        public Game GetGameById(int id)
        {
            var result = _dataContext.Game.FirstOrDefault(x => x.GameId == id);
            return result;
        }

        public IEnumerable<Game> GetGamesBySeasonId(int seasonId)
        {
            var result = _dataContext.Game.Where(x => x.SeasonId == seasonId).AsEnumerable();
            return result;
        }

        public IEnumerable<Game> GetGamesByUserId(string userId)
        {
            var result = _dataContext.Game.Where(x => x.UserId == userId).OrderByDescending(x => x.DatePlayed.Date).AsEnumerable();
            return result;
        }    
    }
}
