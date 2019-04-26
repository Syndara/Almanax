using Almanax.Functions;
using System;

namespace Almanax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current server time is: {DateTime.UtcNow.AddHours(2).ToShortTimeString()}");
            Almanax.Functions.Almanax ax = new Almanax.Functions.Almanax();
            ax.GetOffering();
            while (true)
            {               
                Console.Write("Enter an item to search: ");
                string weapon = Console.ReadLine();
                ItemSearch search = new ItemSearch();
                search.SearchWeapons(weapon);
            }            
        }
    }
}
