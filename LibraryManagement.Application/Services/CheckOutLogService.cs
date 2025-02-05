using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class CheckOutLogService : ICheckOutLog
    {
        private ICheckoutLogRespository _Checkout;
        private IBorrowerRepository _borrowerRepository;
        public CheckOutLogService(ICheckoutLogRespository checkOut, IBorrowerRepository borrower)
        {
            _Checkout = checkOut;
            _borrowerRepository = borrower;
        }

        public Result AddCheckOut(CheckOutLog newCheckOut, string borrowerEmail, int mediaID)
        {
            try
            {
                var CheckOutUser = _borrowerRepository.GetByEmail(borrowerEmail);
                if (CheckOutUser != null)
                {
                    newCheckOut.BorrowerID = CheckOutUser.BorrowerID;
                    var checkOutUserID = newCheckOut.BorrowerID;
                    var totalCheckOuts = _Checkout.GetAllCheckedOut();
                    int counter = 0;
                    bool anyOverDue = false;
                    foreach (var checkOut in totalCheckOuts)
                    {
                        if (checkOut.BorrowerID == checkOutUserID)
                        {
                            counter++;
                        }
                        if (checkOut.BorrowerID == checkOutUserID && checkOut.DueDate < DateTime.Now)
                        {
                            anyOverDue = true;
                        }
                    }
                    if (counter >= 3)
                    {
                        return ResultFactory.Fail("You have reached the maxium checkouts allowed!");
                    }
                    if (anyOverDue)
                    {
                        return ResultFactory.Fail("You have can not rent anymore items while you have an overdue item!");
                    }
                    newCheckOut.MediaID = mediaID;
                    newCheckOut.CheckOutDate = DateTime.Now;
                    newCheckOut.DueDate = DateTime.Today.AddDays(7);
                    _Checkout.AddCheckOut(newCheckOut);
                    return ResultFactory.Success();
                }
                else
                {
                    return ResultFactory.Fail("User email does not exist! ");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result DeleteCheckOut(int checkoutID)
        {
            try
            {
                var log = _Checkout.GetByID(checkoutID);
                //No Return Date == Active CheckOut 
                //Has Return Date == Is already returned
                if(log == null)
                {
                    return ResultFactory.Fail("Invalid ID!");
                }
                if (log.ReturnDate != null)
                {
                    return ResultFactory.Fail("CheckOut already returned and can't be returned again!");
                }
                if (log != null)
                {
                    log.ReturnDate = DateTime.Now;
                    _Checkout.Update(log);
                    return ResultFactory.Success();
                }
                else
                {
                    return ResultFactory.Fail("Unable to find selected checkout...");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result DeleteLogs(int borrowerID)
        {
            try
            {
                var logs = _Checkout.GetAll();
                List<CheckOutLog> logsToDelete = new List<CheckOutLog>();
                foreach (var log in logs)
                {
                    if(log.BorrowerID  == borrowerID)
                    {
                        logsToDelete.Add(log);
                    }
                }
                foreach(var log in logsToDelete)
                {
                    _Checkout.Delete(log);
                }
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Media>> GetAvaliableMedia()
        {
            try
            {
                var medias = _Checkout.GetAvaliableMedia();
                return ResultFactory.Success(medias);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<CheckOutLog>> GetCheckOutLogs()
        {
            try
            {
                var logs = _Checkout.GetAllCheckedOut();

                return ResultFactory.Success(logs);
            }catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckOutLog>>(ex.Message);
            }
        }

        public Result<List<CheckOutLog>> GetCheckOutsForBorrower(int borrowerID)
        {
            try
            {
                var logs = _Checkout.GetAllCheckedOut();
                List<CheckOutLog> userLogs = new();

                foreach (var log in logs)
                {
                    if (log.BorrowerID == borrowerID && log.ReturnDate == null)
                    {
                        userLogs.Add(log);
                    }
                }
                if (userLogs.Count > 0)
                {
                    return ResultFactory.Success(userLogs);
                }
                else
                {
                    return ResultFactory.Fail<List<CheckOutLog>>("User does not currently have any checkouts...");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckOutLog>>(ex.Message);
            }
               
        }

        public Result IsAvaliable(int mediaID)
        {
            try
            {
                var isAvaliable = _Checkout.IsAvaliable(mediaID);

                if (isAvaliable)
                {
                    return ResultFactory.Success();
                }
                else
                {
                    return ResultFactory.Fail("Media is not avaliable! ");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }
    }
}
