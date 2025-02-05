using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ConsoleUI.IO
{
    public class Menus
    {
        public static int BorrowerMenu()
        {
            Console.Clear();

            Console.WriteLine("Borrower Management");
            Console.WriteLine("==========================");
            Console.WriteLine("1. List All Borrowers");
            Console.WriteLine("2. View A Borrower");
            Console.WriteLine("3. Add Borrower");
            Console.WriteLine("4. Edit Borrower");
            Console.WriteLine("5. Delete A Borrower");
            Console.WriteLine("6. Go Back");

            int choice;

            do
            {
                Console.Write("Enter your choice (0-6) : ");
                if(int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        internal static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Library Manager Main Menu\r\n===============\r\n1. Borrower Management\r\n2. Media Management\r\n3. Checkout Management\r\n4. Quit");
            int choice;

            do
            {
                Console.Write("Enter your choice (0-4) : ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        internal static int MediaMenu()
        {

            Console.Clear();

            Console.WriteLine("Media Management\r\n================\r\n1. List Media\r\n2. Add Media\r\n3. Edit Media\r\n4. Archive Media\r\n5. View Archive\r\n6. Most Popular Media Report\r\n7.Go Back");
            int choice;

            do
            {
                Console.Write("Enter your choice (0-6) : ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }
        public static int CheckOutMenu()
        {
            Console.Clear();
            Console.WriteLine("Checkout Management\r\n===================\r\n1. Checkout\r\n2. Return\r\n3. Checkout Log\r\n4. Go Back");
            int choice;
            do
            {
                Console.Write("Enter your choice (0-6) : ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }
    }
}
