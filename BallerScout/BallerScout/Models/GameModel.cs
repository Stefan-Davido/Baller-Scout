using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Models
{
    public class GameModel
    {
        public int GameId { get; set; }
        public DateTime SeasonYear { get; set; }
        public string SeasonYearString { get; set; }
        public int SeasonId { get; set; }
        [Required]
        public DateTime DatePlayed { get; set; }
        public string DatePlayedString { get; set; }
        public string UserId { get; set; }
        [StringLength(150)]
        public string UserFullName { get; set; }
        [StringLength(50), Required]
        public string Club { get; set; }
        [StringLength(50), Required]
        public string VersusClub { get; set; }
        [Range(0, 30)]
        public int GoalsScored { get; set; }
        [Range(0, 30)]
        public int Assists { get; set; }
        [Range(0, 1)]
        public int RedCards { get; set; }
        [Range(0,2)]
        public int YellowCards { get; set; }
        [Range(0, 120), Required]
        public int PlayedMinutes { get; set; }
        [StringLength(30), Required]
        public string PositionPlayed { get; set; }

        // *** Stats for Delete From Season ***
        public int MinutesPlayedDecrementInSeason { get; set; }
        public int GoalsScoredDecrementInSeason { get; set; }
        public int AssistsDecrementInSeason { get; set; }
        public int RedCardsDecrementInSeason { get; set; }
        public int YellowCardsDecrementInSeason { get; set; }
    }
}
