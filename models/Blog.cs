namespace EF_Core;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Blog
{
    [Key]
    public int Id { get; set; } // EF Core looks for Id and make it a PK automaticly
    public string Name { get; set; }
    public string Url { get; set; }
    public List<Blog> GetAll()
    {
        List<Blog> blogs = new List<Blog>();
     
        using(var dbContext = new BloggingContext())
        {
            try
            {
                blogs = dbContext.Blogs.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error While Fetching Blogs from Database: {ex.Message}");
            }
        }
        return blogs; // Return Empty List if Failed
    }
    public void Add(string name, string url)
    {
        using (BloggingContext dbContext = new BloggingContext())
        {
            try
            {
                Blog blog = new Blog()
                {
                    Name = name,
                    Url = url
                };
                
                dbContext.Blogs.Add(blog);
                dbContext.SaveChanges();
                System.Console.WriteLine($"Blog [{name}, {url}] Added");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error While Adding Blog [{name}, {url}] to Database: {ex.Message}");
            }
        }
    }
}
