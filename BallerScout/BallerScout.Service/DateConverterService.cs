using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Service
{
    public class DateConverterService : IDateConverterService
    {
        private readonly ISeasonService _seasonService;
        private readonly IPlayerHistoryService _playerHistoryService;

        public DateConverterService(
            ISeasonService seasonService,
            IPlayerHistoryService playerHistoryService)
        {
            _seasonService = seasonService;
            _playerHistoryService = playerHistoryService;
        }

        public string SeasonYearPlusOneConverter(DateTime datePlayed)
        {
            var date = String.Format("{0:yyyy}", datePlayed);
            var plusOneYear = String.Format("{0:yyyy}", datePlayed.AddYears(1));
            var result = date + "/" + plusOneYear;

            return result;
        }
        public string SeasonYearMinusOneConverter(DateTime datePlayed)
        {
            var date = datePlayed.Year;
            var minusOneYear = date - 1;
            var result = minusOneYear + "/" + date;

            return result;
        }

        public bool NeedNewSeason(DateTime datePlayed)
        {
            DateTime todaysDate = datePlayed.Date;
            var newSeason = DateTime.Parse(String.Format("{0:1-8-yyyy}", todaysDate));
            if (datePlayed > newSeason)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int SeasonOneYearMinusCheck(DateTime datePlayed, string userId)
        {
            var currentYear = datePlayed.Year;
            var previousYear = currentYear - 1;
            string checkYears = previousYear + "/" + currentYear;
            var clubName = _playerHistoryService.GetCurrentClubByUserId(userId);
            int seasonId = 0;

            var seasonsEnum = _seasonService.GetSeasonsByUserId(userId);

            foreach (var season in seasonsEnum)
            {
                if(season.SeasonYearString == checkYears)
                {
                    if(season.Club == clubName)
                    {
                        seasonId = season.SeasonId;
                    }
                    
                }
            }
            return seasonId;
        }
        
        public int SeasonOneYearPlusCheck(DateTime datePlayed, string userId)
        {
            var currentYear = datePlayed.Year;
            var nextYear = currentYear + 1;
            string checkYears = currentYear + "/" + nextYear;
            var clubName = _playerHistoryService.GetCurrentClubByUserId(userId);
            int seasonId = 0;

            var seasonsEnum = _seasonService.GetSeasonsByUserId(userId);

            foreach (var season in seasonsEnum)
            {
                if (season.SeasonYearString == checkYears)
                {
                    if (season.Club == clubName)
                    {
                        seasonId = season.SeasonId;
                    }

                }
            }
            return seasonId;
        }

        public bool CheckSeasonSecondPart(string seasonYears)
        {
            var currentYear = DateTime.Today.Year;
            var nextYear = currentYear + 1;

            string[] yearsArray = seasonYears.Split("/");
            var secondPartOfSeason = int.Parse(yearsArray[1]);

            if (nextYear == secondPartOfSeason)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
