using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO.Workflows
{
    public static class MediaWorkflows
    {
        public static int GetMediaID(IMediaService service)
        {
            Console.Clear();
            Console.WriteLine("Media Types\r\n=====================");
            var result = service.GetMediaTypes();
            if (result.Ok)
            {
                foreach (var mediaType in result.Data!)
                {
                    Console.WriteLine($"{mediaType.MediaTypeID}. {mediaType.MediaTypeName}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Console.WriteLine("Please enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public static void ListAllMedia(IMediaService service)
        {
            Console.Clear();
            var type = GetMediaID(service);
            Console.Clear();
            Console.WriteLine("All Media\r\n=====================");
            Console.WriteLine($"{"ID",-5}{"Title",-32} Avalibility");
            Console.WriteLine(new string('=', 70));
            var result = service.GetAllMedia(type);

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
            Utilities.AnyKey();
        }

        public static void AddMedia(IMediaService service)
        {
            Console.Clear();
            Console.WriteLine("Add Media");
            int mediaType = GetMediaID(service);
            Media media = new Media();
            media.MediaTypeID = mediaType;
            media.Title = Utilities.GetRequiredString("Please enter a title for the Media: ");

            var result = service.AddMedia(media);

            if (result.Ok)
            {
                Console.WriteLine($"Media created with Media id of : {media.MediaID}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }
        public static void ListByMediaType(IMediaService service)
        {
            Console.Clear();
            var mediaID = GetMediaID(service);
            Console.Clear();
            Console.WriteLine("Media By Media Type\r\n=====================");
            Console.WriteLine($"{"ID",-5}{"Title",-32} Avalibility");
            Console.WriteLine(new string('=', 70));
            var result = service.GetMediaByMediaType(mediaID);
            if (result.Ok)
            {
                foreach (var media in result.Data!)
                {
                    Console.WriteLine($"{media.MediaID + ".", -5} {media.Title,-32} {(media.IsArchived ? "Archieved" : "Available")}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        public static void UpdateMedia(IMediaService service)
        {
            Console.Clear();
            ListByMediaType(service);
            var ID = Utilities.GetPositiveInteger("Please enter ID of media to update: ");

            var result = service.GetMedia(ID);
            if (result.Ok)
            {
                result.Data!.Title = Utilities.GetRequiredString($"Please enter new title({result.Data!.Title}): ");
                var result2 = service.UpdateMedia(result.Data!);
                if (result2.Ok)
                {
                    Console.WriteLine("Media has been updated! Changes saved");
                }
                else
                {
                    Console.WriteLine(result2.Message);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }

        public static void ArchieveMedia(IMediaService service)
        {
            Console.Clear();
            ListByMediaType(service);
            var ID = Utilities.GetPositiveInteger("Please enter ID of media to archieve: ");

            var result = service.ArchieveMedia(ID);

            if (result.Ok)
            {
                Console.WriteLine($"Media has been archieved!");
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }
        public static void DisplayArcheveMedia(IMediaService service)
        {
            Console.Clear();
            Console.WriteLine("Media By Media Type\r\n=====================");
            Console.WriteLine($"{"ID",-5}{"Title",-15}{"Media Type",-32} Avalibility");
            Console.WriteLine(new string('=', 70));
            var result = service.GetArchieve();
            if (result.Ok)
            {
                if(result.Data!.Count == 0)
                {
                    Console.WriteLine("There is currently nothing archieved");
                }
                else
                {
                    foreach (var media in result.Data!)
                    {
                        Console.WriteLine($"{media.MediaID + ".",-5} {media.Title,-15} {service.GetMediaType(media.MediaTypeID).Data!.MediaTypeName,-32}{(media.IsArchived ? "Archieved" : "Available")}");
                    }
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }

        public static void DisplayTopMediaReport(IMediaService service)
        {
            Console.Clear();
            Console.WriteLine("Top 3 report!!");
            var result = service.GetTopMediaReport();

            if (result.Ok)
            {
                Console.WriteLine($"Title, CheckOuts");
                Console.WriteLine();
                Console.WriteLine();
                foreach (var media in result.Data!)
                {
                    Console.WriteLine($"{media.Title}, {media.CheckoutCount}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }
    }


}
