using Microsoft.EntityFrameworkCore;
using ProjectBlog.DAL.Entities;

namespace ProjectBlog.DAL.DB
{
    public class BlogContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<Comment>().ToTable("Comments");
        }
    }
}
