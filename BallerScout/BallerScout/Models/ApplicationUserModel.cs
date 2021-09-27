using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Models
{
    public class ApplicationUserModel
    {
        public string PhoneNumber { get; set; }
        public string Id { get; set; }
        [StringLength(150)]
        public string UserName { get; set; }
        public string Email { get; set; }
        [StringLength(60)]
        public string FirstName { get; set; }
        [StringLength(60)]
        public string LastName { get; set; }
        [StringLength(80)]
        public string Country { get; set; }
        public int Age { get; set; }
        [StringLength(80)]
        public string Club { get; set; }
        [StringLength(80)]
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
        public IEnumerable<Post> Posts { get; set; }
        //public Following Following { get; set; }
        public int NumberOfFollowings { get; set; }
        public int NumberOfFollowers { get; set; }
        public int FollowingListId { get; set; }
        public bool FollowingCheckBool { get; set; }
        public string UserId { get; set; }
        public string UserIFollowId { get; set; }
        public DateTime DateCreated { get; set; }
        public int NumberOfPosts { get; set; }
        public Skills Skills{ get; set; }
    }
}
