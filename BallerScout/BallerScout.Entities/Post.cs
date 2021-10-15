using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string UserProfilePhoto{ get; set; }
        [StringLength(150), Required]
        public string UserName{ get; set; }
        public string PhotoUrl{ get; set; }
        [StringLength(500), Required]
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public string DatePostedString { get; set; }
        public Like Like { get; set; }
        public int NumberOfLikes { get; set; }
        public SavedPost SavedPost { get; set; }
        public string UserPosition { get; set; }
        public string UserClub { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
