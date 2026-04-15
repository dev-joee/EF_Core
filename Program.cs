using System;
using EF_Core;
using DataManager;

public class Program
{
    public static void Main(string[] args)
    {
        int choice = 0;

        do
        {
            ShowIntro();

            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);

            if (!isValidChoice) { System.Console.WriteLine("invalid choice, plz try again."); continue; }            
        
            RunSelection(choice);

        } while(!HasQuit());
        
        System.Console.WriteLine("\nThanks for using Blog Manager!\n");
        Environment.Exit(0);
    }

    private static void RunSelection(int choice)
    {
        System.Console.WriteLine($"You Selected {choice}");

        switch (choice)
        {
            case 1:
                DataManager.DataManager.AddBlogSite();
                break;
            case 2:
                DataManager.DataManager.GetAllBlogs();
                break;
            case 3:
                DataManager.DataManager.AddPost();
                break;
            case 4:
                DataManager.DataManager.GetAllPosts();
                break;
            case 5:
                DataManager.DataManager.UpdatePost();
                break;
            case 6:
                DataManager.DataManager.DeletePost();
                break;
            default:
                System.Console.WriteLine("out of range, plz try again.");
                break;
        }        
    }
    private static void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Blog Manager! What would you like to do? \n");
        Console.WriteLine("1. Add a blog site");
        Console.WriteLine("2. Get all blogs");
        Console.WriteLine("3. Add a post");
        Console.WriteLine("4. Get all posts");
        Console.WriteLine("5. Update a post");
        Console.WriteLine("6. Delete a post");
        
        Console.Write("\nSelect a number [1 - 6]: ");        
    }
    private static bool HasQuit()
    {
        bool isValidInput = false; // does not matter the initial value
        bool HasQuit = false;

        do
        {
            System.Console.Write("Do you want to continue? [Y/n]: ");
            string input = Console.ReadLine() ?? string.Empty;
            input = input.ToLower();

            if (input.Equals("y") || input.Equals(string.Empty))
            {
                isValidInput = true;
                HasQuit = false;
            }
            else if (input.Equals("n"))
            {
                isValidInput = true;
                HasQuit = true;
            }    
            else
            {
                isValidInput = false;
                System.Console.WriteLine("invalid input, plz try again.");
            }
        } while (!isValidInput);

        return HasQuit;
    }
}