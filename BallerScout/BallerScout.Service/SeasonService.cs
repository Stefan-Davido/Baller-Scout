using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public void AddSeason(Season season)
        {
            _seasonRepository.AddSeason(season);
        }

        public void DeleteSeason(int id)
        {
            _seasonRepository.DeleteSeason(id);
        }

        public void UpdateSeason(Season season)
        {
            _seasonRepository.UpdateSeason(season);
        }

        public Season GetSeasonById(int id)
        {
            var result = _seasonRepository.GetSeasonById(id);
            return result;
        }

        public IEnumerable<Season> GetSeasonsByUserId(string userId)
        {
            var result = _seasonRepository.GetSeasonsByUserId(userId);
            return result;
        }
    }
}
