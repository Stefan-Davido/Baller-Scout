using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Service
{
    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly IPlayerHistoryRepository _playerHistoryRepository;

        public PlayerHistoryService(IPlayerHistoryRepository playerHistoryRepository)
        {
            _playerHistoryRepository = playerHistoryRepository;
        }

        public void AddPlayerHistory(PlayerHistory playerHistory)
        {
            _playerHistoryRepository.AddPlayerHistory(playerHistory);
        }

        public void DeletePlayerHistory(int id)
        {
            _playerHistoryRepository.DeletePlayerHistory(id);
        }

        public IEnumerable<PlayerHistory> GetPlayerHistoriesByUserId(string id)
        {
            var result = _playerHistoryRepository.GetPlayerHistoriesByUserId(id);
            return result;
        }

        public PlayerHistory GetPlayerHistoryById(int id)
        {
            var result = _playerHistoryRepository.GetPlayerHistoryById(id);
            return result;
        }

        public void UpdatePlayerHistory(PlayerHistory playerHistory)
        {
            _playerHistoryRepository.UpdatePlayerHistory(playerHistory);
        }

        public PlayerHistory GetLastPlayerHistoryByUserId(string id)
        {
            var result = _playerHistoryRepository.GetLastPlayerHistoryByUserId(id);
            return result;
        }

        public string GetCurrentClubByUserId(string userId)
        {
            var transfers = GetPlayerHistoriesByUserId(userId);
            var lastTransfer = transfers.OrderByDescending(x => x.TransferDate.Date).FirstOrDefault();
            string clubName = "Unknown";

            if (lastTransfer != null)
            {
                if (lastTransfer.TransferDate != null)
                {
                    clubName = lastTransfer.ToClub;
                }

            }           

            return clubName;
        }
    }
}
