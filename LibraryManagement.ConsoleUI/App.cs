using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application;
using LibraryManagement.ConsoleUI.IO;
using LibraryManagement.ConsoleUI.IO.Workflows;
using LibraryManagement.Core.Interfaces.Application;

namespace LibraryManagement.ConsoleUI
{
    public class App
    {
        IAppConfiguration _configuration;
        SerivceFactory _serivceFactory;

        public App(IAppConfiguration configuration)
        {
            _configuration = new AppConfiguration();
            _serivceFactory = new SerivceFactory(configuration);
        }

        public void Run()
        {
            do
            {
                int mainSelection = Menus.MainMenu();

                if(mainSelection == 1)
                {
                    bool letsQuit = false;
                    do
                    {
                        int choice = Menus.BorrowerMenu();
                        
                        switch (choice)
                        {
                            case 6:
                                letsQuit = true;
                                break;
                            case 1:
                                BorrowerWorkflows.GetAllBorrowers(_serivceFactory.CreateBorrowerService());
                                break;
                            case 2:
                                BorrowerWorkflows.GetBorrowerByID(_serivceFactory.CreateBorrowerService(), _serivceFactory.CreateCheckOutService(), _serivceFactory.CreateMediaService());
                                break;
                            case 3:
                                BorrowerWorkflows.AddBorrower(_serivceFactory.CreateBorrowerService());
                                break;
                            case 4:
                                BorrowerWorkflows.UpdateBorrower(_serivceFactory.CreateBorrowerService());
                                break;
                            case 5:
                                BorrowerWorkflows.RemoveBorrower(_serivceFactory.CreateBorrowerService(),_serivceFactory.CreateCheckOutService());
                                break;
                        }
                        if (letsQuit)
                        {
                            break;
                        }
                    } while (true);
                }else if(mainSelection == 2)
                {
                    bool letsQuit = false;
                    do
                    {
                        int choice = Menus.MediaMenu();

                        switch (choice)
                        {
                            case 7:
                                letsQuit = true;
                                break;
                            case 1:
                                MediaWorkflows.ListAllMedia(_serivceFactory.CreateMediaService());
                                break;
                            case 2:
                                MediaWorkflows.AddMedia(_serivceFactory.CreateMediaService());
                                break;
                            case 3:
                                MediaWorkflows.UpdateMedia(_serivceFactory.CreateMediaService());
                                break;
                            case 4: 
                                MediaWorkflows.ArchieveMedia(_serivceFactory.CreateMediaService());
                                break;
                            case 5: 
                                MediaWorkflows.DisplayArcheveMedia(_serivceFactory.CreateMediaService());
                                break;
                            case 6:
                                MediaWorkflows.DisplayTopMediaReport(_serivceFactory.CreateMediaService());
                                break;
                        }
                        if (letsQuit)
                        {
                            break;
                        }
                    } while (true);
                }else if(mainSelection == 3)
                {
                    do
                    {


                        bool letsQuit = false;

                        int choice = Menus.CheckOutMenu();

                        switch (choice)
                        {
                            case 4:
                                letsQuit = true;
                                break;
                            case 1:
                                CheckoutWorkflows.PerformCheckOut(_serivceFactory.CreateCheckOutService(), _serivceFactory.CreateMediaService());
                                break;
                            case 2:
                                CheckoutWorkflows.ReturnCheckOut(_serivceFactory.CreateCheckOutService(), _serivceFactory.CreateMediaService());
                                break;
                            case 3:
                                CheckoutWorkflows.PrintCheckOutLog(_serivceFactory.CreateCheckOutService(), _serivceFactory.CreateMediaService(), _serivceFactory.CreateBorrowerService());
                                break;
                        }
                        if (letsQuit)
                        {
                            break;
                        }
                    } while (true);
                }
                else
                {
                    Console.WriteLine("Have a great day!");
                    break;
                }
            } while (true);
        }
    }
}
