using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void AddGame(Game game)
        {
            _gameRepository.AddGame(game);
        }

        public void UpdateGame(Game game)
        {
            _gameRepository.UpdateGame(game);
        }

        public void DeleteGame(int id)
        {
            _gameRepository.DeleteGame(id);
        }

        public Game GetGameById(int id)
        {
            var result = _gameRepository.GetGameById(id);
            return result;
        }

        public IEnumerable<Game> GetGamesBySeasonId(int seasonId)
        {
            var result = _gameRepository.GetGamesBySeasonId(seasonId);
            return result;
        }

        public IEnumerable<Game> GetGamesByUserId(string userId)
        {
            var result = _gameRepository.GetGamesByUserId(userId);
            return result;
        }
    }
}
