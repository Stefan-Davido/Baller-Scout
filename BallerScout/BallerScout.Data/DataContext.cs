using BallerScout.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BallerScout.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Following> Following{ get; set; }
        public DbSet<Like> Like{ get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }
        public DbSet<Skills> Skills{ get; set; }
        public DbSet<PlayerHistory> PlayerHistory{ get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Game> Game{ get; set; }

    }
}
