namespace DataManager;

using System;
using System.Runtime.Intrinsics.Arm;
using EF_Core;
using Microsoft.VisualBasic;

// UI <-> DataManager <-> Modles <-> BloggingContext <-> Database 


public static class DataManager
{
    private static Blog _blog = new Blog();
    private static Post _post = new Post();

    public static void AddBlogSite()
    {
        Console.Clear();
        System.Console.WriteLine("=== Add Blog Site ===\n");

        System.Console.Write("Entre Blog Name: ");
        string name = Console.ReadLine() ?? string.Empty;

        System.Console.Write("Entre Blog Url: ");
        string url = Console.ReadLine() ?? string.Empty;

        _blog.Add(name, url);
    }
    
    public static void GetAllBlogs()
    {
        Console.Clear();
        System.Console.WriteLine("=== Get All Blogs ===\n");

        List<Blog> blogs = _blog.GetAll();
        
        if (blogs.Count == 0)
        {
            System.Console.WriteLine("There are no Blogs yet!\n");
            return;
        }

        System.Console.WriteLine("---");

        foreach (var blog in blogs)
        {
            System.Console.WriteLine($"blog id: {blog.Id}");
            System.Console.WriteLine($"blog name: {blog.Name}");
            System.Console.WriteLine($"blog url: {blog.Url}");
            System.Console.WriteLine("---");
        }

        System.Console.WriteLine("---");
    } 
    
    public static void AddPost()
    {
        Console.Clear();
        System.Console.WriteLine("=== Add Post ===\n");

        var AllBlogs = _blog.GetAll();

        if (AllBlogs.Count == 0)
        {
            System.Console.WriteLine("There Are No Blogs, You Can Not Add a Post.\n");
            return;            
        }

        Console.Write("Entre post title: ");
        string title = Console.ReadLine() ?? string.Empty;

        Console.Write("Entre post content: ");
        string content = Console.ReadLine() ?? string.Empty;


        int blog_id = 0;
        bool valid_blog_id = false;
        
        do
        {
            System.Console.WriteLine("All Avalable Blogs");

            GetAllBlogs();

            Console.Write("Select the Blog you want to add this post to: ");
            valid_blog_id = int.TryParse(Console.ReadLine(), out blog_id);
            
            if (!valid_blog_id) { System.Console.WriteLine("invalid blog id, plz try again.\n"); continue; } // invalid numrical conversion

            if (!AllBlogs.Any(p => p.Id == blog_id)) // there is no blog id matches the input blog id
            {
                valid_blog_id = false;
            }

        } while (!valid_blog_id);

        _post.Add(title, content, blog_id);
    } 
    public static void GetAllPosts()
    {
        Console.Clear();
        System.Console.WriteLine("=== Get All Posts ===\n");

        List<Post> posts = _post.GetAll();

        if (posts.Count == 0)
        {
            System.Console.WriteLine("There are no posts yet!\n");
            return;
        }

        System.Console.WriteLine("---");

        foreach (var post in posts)
        {
            System.Console.WriteLine($"id: {post.Id}");
            System.Console.WriteLine($"title: {post.Title}");
            System.Console.WriteLine($"content: {post.Content}");
            System.Console.WriteLine($"Blog Id: {post.BlogId}");
            System.Console.WriteLine("---");
        }

        System.Console.WriteLine("---\n");
    }

    public static void UpdatePost()
    {
        Console.Clear();
        System.Console.WriteLine("=== Update Post ===\n");        

        int post_id = 0;
        bool isValidId = false;

        do
        {
            System.Console.Write("Entre post id you want to update: ");
            isValidId = int.TryParse(Console.ReadLine(), out post_id);

            if(!isValidId)
            {
                System.Console.WriteLine("invalid id, plz try again.\n");    
            }

        } while(!isValidId);

        System.Console.Write("Entre New Post Title: ");
        string title = Console.ReadLine();

        System.Console.Write("Entre New Post Content: ");
        string content = Console.ReadLine();

        _post.Update(post_id, title, content);

        System.Console.WriteLine("");
    }

    public static void DeletePost()
    {
        Console.Clear();
        System.Console.WriteLine("=== Delete Post ===\n");

        int post_id = 0;
        bool isValidId = false;

        do
        {
            System.Console.Write("Entre post id you want to delete: ");
            isValidId = int.TryParse(Console.ReadLine(), out post_id);

            if(!isValidId)
            {
                System.Console.WriteLine("invalid id, plz try again.\n");    
            }

        } while(!isValidId);
        
        var posts = _post.GetAll();
        var post = posts.FirstOrDefault(p => p.Id == post_id); 

        if (post == null)
        {
            System.Console.WriteLine($"Post Id {post_id} does not exist\n");
            return;
        }
        _post.Delete(post);

        System.Console.WriteLine("");
    }
}
