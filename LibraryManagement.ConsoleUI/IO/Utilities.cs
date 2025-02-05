using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public class Utilities
    {
        public static void AnyKey()
        {
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }

        public static int GetPositiveInteger(string prompt)
        {
            int result;

            do
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Invalid input, must be a positive integer!");
            } while (true);
        }
        public static string GetRequiredString(string prompt)
        {
            string? input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input)){
                    return input;
                }

                Console.WriteLine("Input is required");
                AnyKey();
            } while (true);
        }
        public static int GetFromAllMedia(IMediaService service)
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("All Media\r\n=====================");
            Console.WriteLine($"{"ID",-5}{"Title",-32} Avalibility");
            Console.WriteLine(new string('=', 70));
            var result = service.GetAll();

            if (result.Ok)
            {
                foreach (var media in result.Data!)
                {
                    Console.WriteLine($"{media.MediaID + ".",-5} {media.Title,-32} {(media.IsArchived ? "Archieved" : "Available")}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            return GetPositiveInteger("Please select media that you would like to checkout!");
        }
    }
}
