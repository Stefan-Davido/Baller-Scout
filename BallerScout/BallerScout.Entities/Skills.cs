using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class Skills
    {
        [Key]
        public int SkillStatsId { get; set; }
        public string UserId { get; set; }
        [StringLength(150), Required]
        public string UserFullName { get; set; }
        [Range(1,10)]
        public int Dribbling { get; set; }
        [Range(1, 10)]
        public int Shooting { get; set; }
        [Range(1, 10)]
        public int Passing { get; set; }
        [Range(1, 10)]
        public int Pace { get; set; }
        [Range(1, 10)]
        public int Defending { get; set; }
        [Range(1, 10)]
        public int Physical { get; set; }

        // *** For Goalkeeper ***
        [Range(1, 10)]
        public int Diving { get; set; }
        [Range(1, 10)]
        public int Handling { get; set; }
        [Range(1, 10)]
        public int Kicking { get; set; }
        [Range(1, 10)]
        public int Reflexes { get; set; }
        [Range(1, 10)]
        public int Speed { get; set; }
        [Range(1, 10)]
        public int Positioning { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; } 
    }
}
