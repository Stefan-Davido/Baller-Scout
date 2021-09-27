using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int PostId{ get; set; }
        public string UserPostId { get; set; }
        [StringLength(150), Required]
        public string UserName { get; set; }
        public string UserLikedId { get; set; }
        [StringLength(150), Required]
        public string UserLikedName { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
