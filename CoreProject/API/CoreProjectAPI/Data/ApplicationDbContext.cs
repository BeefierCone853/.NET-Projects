using CoreProjectAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoreProjectAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
    }
}