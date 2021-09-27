using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class SavedPost
    {
        [Key]
        public int SavingId{ get; set; }
        public int PostId{ get; set; }
        public string UserPostId{ get; set; }
        [StringLength(150), Required]
        public string UserPostName{ get; set; }
        public string UserSaveId{ get; set; }
        [StringLength(150), Required]
        public string UserSaveName{ get; set; }
        public DateTime DateSaved{ get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
