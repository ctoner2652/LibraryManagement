using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO.Workflows
{
    public class BorrowerWorkflows
    {
        public static void GetAllBorrowers(IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("Borrower list");
            Console.WriteLine($"{"ID",-5}{"Name",-32} Email");
            Console.WriteLine(new string('=', 70));
            var result = borrowerService.GetAllBorrowers();

            if (result.Ok)
            {
                foreach (var b in result.Data)
                {
                    Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void GetBorrowerByID(IBorrowerService service, ICheckOutLog logService, IMediaService mediaService)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = service.GetBorrower(email);

            if (result.Ok)
            {
                Console.WriteLine("\nBorrower Information");
                Console.WriteLine("======================");
                Console.WriteLine($"Id: {result.Data.BorrowerID}");
                Console.WriteLine($"Name: {result.Data.LastName}, {result.Data.FirstName}");
                Console.WriteLine($"Email: {result.Data.Email}");
                var checkResult = logService.GetCheckOutsForBorrower(result.Data.BorrowerID);

                if (checkResult.Ok)
                {
                    Console.WriteLine("User's current Check Outs...");
                    foreach(var log in checkResult.Data!)
                    {
                        Console.WriteLine($"Title: {mediaService.GetMedia(log.MediaID).Data!.Title} and Due On {log.DueDate}");
                    }
                }
                else
                {
                    Console.WriteLine(checkResult.Message);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            Utilities.AnyKey();
        }

        public static void AddBorrower(IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("Add New Borrower");
            Console.WriteLine("================");
            Borrower newBorrower = new Borrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var result = borrowerService.AddBorrower(newBorrower);

            if (result.Ok)
            {
                Console.WriteLine($"Borrower created with borrower id of : {newBorrower.BorrowerID}");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }
        public static void UpdateBorrower(IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("Edit Borrower");
            Console.WriteLine("================");
            Borrower editBorrower = new Borrower();
            var result = borrowerService.GetBorrower(Utilities.GetRequiredString("Please enter borrowers email: "));
            if (result.Ok)
            {
                editBorrower = result.Data!;
                Console.WriteLine($"Current First Name: {editBorrower.FirstName}...");
                editBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
                Console.WriteLine($"Current Last Name: {editBorrower.LastName}...");
                editBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
                Console.WriteLine($"Current Email:  {editBorrower.Email}...");
                editBorrower.Email = Utilities.GetRequiredString("Email: ");
                Console.WriteLine($"Current Phone #: {editBorrower.Phone}...");
                editBorrower.Phone = Utilities.GetRequiredString("Phone: ");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            var result2 = borrowerService.UpdateBorrower(editBorrower);
            if (result2.Ok)
            {
                Console.WriteLine($"{result2.Data.FirstName}, {result2.Data.LastName} has been updated!");
            }
            else
            {
                Console.WriteLine(result2.Message);
            }

            Utilities.AnyKey();
        }

        public static void RemoveBorrower(IBorrowerService borrowerService, ICheckOutLog logService)
        {
            Console.Clear();
            Console.WriteLine("Remove Borrower");
            Console.WriteLine("================");

            var userToBeRemoved = borrowerService.GetBorrower(Utilities.GetRequiredString("Enter borrowers email : "));
            if (userToBeRemoved.Ok)
            {
                Console.WriteLine($"Are you sure you would like to remove: {userToBeRemoved.Data.FirstName} {userToBeRemoved.Data.LastName}");
                string var = Console.ReadLine().ToUpper();
                if (var == "YES")
                {
                    
                        var checkresult = logService.DeleteLogs(userToBeRemoved.Data.BorrowerID);
                        if (checkresult.Ok)
                        {
                            var result = borrowerService.RemoveBorrower(userToBeRemoved.Data.Email);
                            if (result.Ok)
                            {
                                Console.WriteLine($"{result.Data} has been removed along with their logs");
                        }
                        else
                        {
                            Console.WriteLine(result.Message);
                        }
                        }
                        else
                        {
                            Console.WriteLine(checkresult.Message);
                        }
                        
                  

                }
                else
                {
                    Console.WriteLine("User deletion aborted");
                }
            }
            else
            {
                Console.WriteLine(userToBeRemoved.Message);
            }

            Utilities.AnyKey();

        }
    }
}
