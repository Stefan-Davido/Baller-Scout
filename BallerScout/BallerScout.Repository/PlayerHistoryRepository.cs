using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class PlayerHistoryRepository : IPlayerHistoryRepository
    {
        private readonly DataContext _dataContext;

        public PlayerHistoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddPlayerHistory(PlayerHistory playerHistory)
        {
            _dataContext.PlayerHistory.Add(playerHistory);
            _dataContext.SaveChanges();
        }

        public void DeletePlayerHistory(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerHistory(PlayerHistory playerHistory)
        {
            _dataContext.PlayerHistory.Update(playerHistory);
            _dataContext.SaveChanges();
        }

        public PlayerHistory GetPlayerHistoryById(int id)
        {
            var result = _dataContext.PlayerHistory.FirstOrDefault(x => x.HistoryId == id);
            return result;
        }

        public IEnumerable<PlayerHistory> GetPlayerHistoriesByUserId(string id)
        {
            var result = _dataContext.PlayerHistory.Where(x => x.UserId == id).OrderByDescending(x => x.TransferDate.Date).AsEnumerable();
            return result;
        }

        public PlayerHistory GetLastPlayerHistoryByUserId(string id)
        {
            var result = _dataContext.PlayerHistory.LastOrDefault(x => x.UserId == id);
            return result;
        }
    }
}
