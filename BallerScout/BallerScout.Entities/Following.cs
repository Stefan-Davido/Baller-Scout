using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class Following
    {
        [Key]
        public int FollowingListId { get; set; }
        public string UserId { get; set; }
        [StringLength(150), Required]
        public string UserName { get; set; }
        public bool FollowingBool { get; set; }
        public string UserIFollowId{ get; set; }
        [StringLength(150), Required]
        public string UserIFollowName{ get; set; }
        public DateTime DateCreated{ get; set; }
        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
