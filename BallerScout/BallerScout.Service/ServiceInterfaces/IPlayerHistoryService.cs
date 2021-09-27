using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IPlayerHistoryService
    {
        void AddPlayerHistory(PlayerHistory playerHistory);
        void UpdatePlayerHistory(PlayerHistory playerHistory);
        void DeletePlayerHistory(int id);
        PlayerHistory GetPlayerHistoryById(int id);
        IEnumerable<PlayerHistory> GetPlayerHistoriesByUserId(string id);
        PlayerHistory GetLastPlayerHistoryByUserId(string id);

        string GetCurrentClubByUserId(string userId);
    }
}
