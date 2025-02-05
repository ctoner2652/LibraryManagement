using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repository
{
    public interface ICheckoutLogRespository
    {
        List<CheckOutLog> GetAllCheckedOut();
        void AddCheckOut(CheckOutLog checkOut);
        List<Media> GetAvaliableMedia();
        bool IsAvaliable(int mediaID);
        void Update(CheckOutLog Checkout);
        CheckOutLog GetByID(int checkOutID);
        void Delete(CheckOutLog Checkout);
        List<CheckOutLog> GetAll();
    }
}
