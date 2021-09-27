using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface ISeasonRepository
    {
        void AddSeason(Season season);
        void UpdateSeason(Season season);
        void DeleteSeason(int id);
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasonsByUserId(string id);
    }
}
