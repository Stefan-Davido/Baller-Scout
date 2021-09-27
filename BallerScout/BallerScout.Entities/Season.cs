using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class Season
    {
        [Key]
        public int SeasonId { get; set; }
        public DateTime SeasonYear { get; set; }
        public string SeasonYearString { get; set; }
        public string UserId { get; set; }
        [StringLength(150), Required]
        public string UserFullName { get; set; }
        [StringLength(80), Required]
        public  string Club { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
        public int GamesPlayed { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
