using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO.Workflows
{
    public class CheckoutWorkflows
    {
        public static void PerformCheckOut(ICheckOutLog service, IMediaService mediaService)
        {
            Console.Clear();
            var usersEmail = Utilities.GetRequiredString("Please enter borrowers email: ");
            do
            {
                var mediaID = ListAllMedia(service);
                var result = service.IsAvaliable(mediaID);
                CheckOutLog newLog = new CheckOutLog();
                if (result.Ok)
                {
                    var result2 = service.AddCheckOut(newLog, usersEmail, mediaID);
                    if (result2.Ok)
                    {
                        Console.WriteLine("Media has been checked out!");
                        Console.WriteLine("Would you like to checkout another item? Y/N");
                        var answer = Console.ReadLine().ToUpper();
                        if(answer == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(result2.Message);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                }

            } while (true);
            Utilities.AnyKey();
           

        }
        public static int ListAllMedia(ICheckOutLog log)
        {
            Console.Clear();
            Console.WriteLine("All Media\r\n=====================");
            Console.WriteLine($"{"ID",-5}{"Title",-32} Avalibility");
            Console.WriteLine(new string('=', 70));
            var result = log.GetAvaliableMedia();

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

            return Utilities.GetPositiveInteger("Please select a media ID: ");
        }

        public static void ListAllCheckOuts(ICheckOutLog service, IMediaService mediaService)
        {
            Console.Clear();
            Console.WriteLine("===All Active Check Outs ===");
            Console.WriteLine($"{"ID",-5}{"Title",-32} DueDate");
            Console.WriteLine(new string('=', 70));
            var result = service.GetCheckOutLogs();

            if (result.Ok)
            {
                foreach(var log in result.Data!)
                {
                    Console.WriteLine($"{log.CheckOutLogID,-5}{mediaService.GetMedia(log.MediaID).Data!.Title,-32}{log.DueDate}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

        }

        public static void ReturnCheckOut(ICheckOutLog log, IMediaService mediaService)
        {
            Console.Clear();
            ListAllCheckOuts(log, mediaService);
            var CheckOutID = Utilities.GetPositiveInteger("Enter CheckOutLog ID");

            var result = log.DeleteCheckOut(CheckOutID);

            if (result.Ok)
            {
                Console.WriteLine("CheckOut Item has been returned!");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void PrintCheckOutLog(ICheckOutLog service, IMediaService mediaService, IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("===All Active Check Outs ===");
            Console.WriteLine($"{"Borrowers Name",-10}{"Borrowers Email",-30} {"Media Title", -40} {"CheckoutDate",-45} DueDate");
            Console.WriteLine(new string('=', 70));
            var result = service.GetCheckOutLogs();

            if (result.Ok)
            {
                foreach (var log in result.Data!)
                {
                    Console.WriteLine($"{borrowerService.GetByID(log.BorrowerID).Data.FirstName + " " + borrowerService.GetByID(log.BorrowerID).Data.LastName,-10}" +
                        $"{borrowerService.GetByID(log.BorrowerID).Data.Email,-30}" +
                        $"{mediaService.GetMedia(log.MediaID).Data.Title,-40}{log.CheckOutDate, -45}{log.DueDate}");
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
