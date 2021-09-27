using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class PlayerHistory
    {
        [Key]
        public int HistoryId { get; set; }  
        public string UserId { get; set; }
        [StringLength(150), Required]
        public string UserFullName { get; set; }
        [StringLength(80), Required]
        public string FromClub { get; set; }
        [StringLength(80), Required]
        public string ToClub { get; set; }
        [StringLength(40), Required]
        public string ContractType { get; set; }
        public DateTime TransferDate { get; set; }
        public string TransferDateString { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
