using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Models
{
    public class UserExperienceModel
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public IEnumerable<Game> UserGames { get; set;}
        public IEnumerable<PlayerHistory> UserTransfers { get; set;}
        public IEnumerable<Season> UserSeasons { get; set;}
    }
}
