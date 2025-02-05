using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface ICheckOutLog
    {
        Result AddCheckOut(CheckOutLog newCheckOut, string borrowerEmail, int mediaID);
        Result DeleteCheckOut(int checkoutID);
        Result<List<CheckOutLog>> GetCheckOutLogs();
        Result<List<Media>> GetAvaliableMedia();
        Result IsAvaliable(int mediaID);
        Result<List<CheckOutLog>> GetCheckOutsForBorrower(int borrowerID);
        Result DeleteLogs(int borrowerID);
    }
}
