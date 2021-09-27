using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly DataContext _dataContext;

        public SeasonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddSeason(Season season)
        {
            _dataContext.Season.Add(season);
            _dataContext.SaveChanges();
        }

        public void UpdateSeason(Season season)
        {
            _dataContext.Season.Update(season);
            _dataContext.SaveChanges();
        }

        public void DeleteSeason(int id)
        {
            throw new NotImplementedException();
        }

        public Season GetSeasonById(int id)
        {
            var result = _dataContext.Season.FirstOrDefault(x => x.SeasonId == id);
            return result;
        }

        public IEnumerable<Season> GetSeasonsByUserId(string id)
        {
            var result = _dataContext.Season.Where(x => x.UserId == id).OrderByDescending(x => x.SeasonYear.Date).AsEnumerable();
            return result;
        }
    }
}
