using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(int id);
        Game GetGameById(int id);
        IEnumerable<Game> GetGamesBySeasonId(int seasonId);
        IEnumerable<Game> GetGamesByUserId (string userId);
    }
}
