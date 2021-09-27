using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BallerScout.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(30), Required]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(60)]
        public string Country { get; set; }
        [Range(1, 99)]
        public int Age { get; set; }
        [StringLength(60)]
        public string Club { get; set; }
        [StringLength(60)]
        public string City { get; set; }
        [Range(0.60, 2.30)]
        public double Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Foot { get; set; }
        [StringLength(50)]
        public string Cityzenship { get; set; }
        [StringLength(500)]
        public string About { get; set; }
        public string ImgURL { get; set; }
        public Post Post { get; set; }
        public Following Following { get; set; }
        public int NumberOfFollowings { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfSavedPosts { get; set; }

        //public int FollowingListId{ get; set; }
    }
}
