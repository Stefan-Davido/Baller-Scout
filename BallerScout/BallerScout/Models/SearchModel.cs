using BallerScout.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Models
{
    public class SearchModel
    {
        public string SearchString{ get; set; }
        public IEnumerable<Post> AllPosts { get; set; }
        public IEnumerable<ApplicationUser> SearchResult { get; set; }
    }
}
