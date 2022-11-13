using HPhoto.Model;
using Microsoft.EntityFrameworkCore;

namespace HPhoto.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser => Set<ApplicationUser>();
        
        public DbSet<User> Users { get; set; }  
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();

    }
}
