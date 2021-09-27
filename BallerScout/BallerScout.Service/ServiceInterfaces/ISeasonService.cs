using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface ISeasonService
    {
        void AddSeason(Season season);
        void UpdateSeason(Season season);
        void DeleteSeason(int id);
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasonsByUserId(string userId);
    }
}
