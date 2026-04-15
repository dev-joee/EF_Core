//Here we define database connection configration

namespace EF_Core;

using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=db/sqlite.db");
}
