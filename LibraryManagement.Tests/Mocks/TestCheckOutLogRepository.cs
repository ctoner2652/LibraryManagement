using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;

namespace LibraryManagement.Tests.Mocks
{
    public class TestCheckOutLogRepository : ICheckoutLogRespository
    {
        public void AddCheckOut(CheckOutLog checkOut)
        {
            throw new NotImplementedException();
        }

        public void Delete(CheckOutLog Checkout)
        {
            throw new NotImplementedException();
        }

        public List<CheckOutLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<CheckOutLog> GetAllCheckedOut()
        {
            throw new NotImplementedException();
        }

        public List<Media> GetAvaliableMedia()
        {
            throw new NotImplementedException();
        }

        public CheckOutLog GetByID(int checkOutID)
        {
            throw new NotImplementedException();
        }

        public bool IsAvaliable(int mediaID)
        {
            throw new NotImplementedException();
        }

        public void Update(CheckOutLog Checkout)
        {
            throw new NotImplementedException();
        }
    }
}
