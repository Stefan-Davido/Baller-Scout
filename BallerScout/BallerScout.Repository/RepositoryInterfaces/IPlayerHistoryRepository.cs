using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface IPlayerHistoryRepository
    {
        void AddPlayerHistory(PlayerHistory playerHistory);
        void UpdatePlayerHistory(PlayerHistory playerHistory);
        void DeletePlayerHistory(int id);
        PlayerHistory GetPlayerHistoryById(int id);
        IEnumerable<PlayerHistory> GetPlayerHistoriesByUserId(string id);
        PlayerHistory GetLastPlayerHistoryByUserId(string id);
    }
}
