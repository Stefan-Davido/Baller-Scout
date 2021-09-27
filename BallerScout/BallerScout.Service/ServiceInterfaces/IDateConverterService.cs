using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IDateConverterService
    {
        string SeasonYearPlusOneConverter(DateTime dateNow);
        string SeasonYearMinusOneConverter(DateTime dateNow);
        bool NeedNewSeason(DateTime dateNow);
        bool CheckSeasonSecondPart(string seasonYears);
        int SeasonOneYearMinusCheck(DateTime datePlayed, string userId);
        int SeasonOneYearPlusCheck(DateTime datePlayed, string userId);
    }
}
