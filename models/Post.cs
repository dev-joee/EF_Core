namespace EF_Core;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    // relations (Blog 1 --- M Post)
    // One to Many Relationshipt (blog can have many posts) -> Add Blog PK as a FK in Post
    [ForeignKey("Blog")]
    public int BlogId { get; set; }
    public Blog? Blog { get; set; } // used to return the post's associated blog

    public List<Post> GetAll()
    {
        List<Post> posts = new List<Post>();
        using (var dbContext = new BloggingContext())
        {
            try
            {
                posts = dbContext.Posts.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while fetching Psots from Database, Error Message: {ex.Message}");                
            }
        }
        return posts;
    }
    public List<Post> GetPostsFor(int blogId)
    {
        List<Post> posts = new List<Post>();
        using (var dbContext = new BloggingContext())
        {
            try
            {
                System.Console.WriteLine($"Blog Id [{blogId}]");
                posts = dbContext.Posts
                        .Where(p => p.BlogId == blogId)
                        .Include(p => p.Blog)
                        .ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while fetching Psots for Blod Id [{blogId}] from Database, Error Message: {ex.Message}");                
            }
        }
        return posts;
    }
    public Post GetPost(int PostId)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = new Post();
                post = dbContext.Posts.FirstOrDefault(p => p.Id == PostId);

                if (post != null)
                {
                    return post;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while fetching Psot With Post Id [{PostId}] from Database, Error Message: {ex.Message}");                
            }
        }
        return new Post();
    }

    public void Add(string title, string content, int blogId)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = new Post()
                {
                    Title = title,
                    Content = content,
                    BlogId = blogId
                };

                dbContext.Add(post);
                dbContext.SaveChanges();
                System.Console.WriteLine("Post Added");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while Adding Psot [{title}] to Database, Error Message: {ex.Message}");                
            }
        }
    }

    public void Update(int postId, string title, string content)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(p => p.Id == postId);

                if (post == null)
                {
                    System.Console.WriteLine("Post Not Found");    
                    return;
                }

                post.Title = title;
                post.Content = content;

                dbContext.SaveChanges();
                System.Console.WriteLine("Post Updated");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while Updating Psot [{title}] to Database, Error Message: {ex.Message}");                
            }
        }        
    }
    public void Delete(Post post)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                dbContext.Posts.Remove(post);
                dbContext.SaveChanges();
                System.Console.WriteLine("Post Daleted");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error while Deleting Psot [{post.Title}] from Database, Error Message: {ex.Message}");                
            }
        }        
    }
}