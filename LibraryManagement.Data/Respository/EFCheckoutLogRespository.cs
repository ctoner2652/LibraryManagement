using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Respository
{
    public class EFCheckoutLogRespository : ICheckoutLogRespository
    {
        private LibraryContext _dbContext; 

        public EFCheckoutLogRespository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void AddCheckOut(CheckOutLog checkOut)
        {
            _dbContext.CheckOutLog.Add(checkOut);
            _dbContext.SaveChanges();
        }

        public List<CheckOutLog> GetAllCheckedOut()
        {
            return _dbContext.CheckOutLog.Where(l => l.ReturnDate == null).ToList();
        }

        public List<CheckOutLog> GetAll()
        {
            return _dbContext.CheckOutLog.ToList();
        }
        public List<Media> GetAvaliableMedia()
        {
            return _dbContext.Media
            .FromSqlRaw("SELECT * FROM Media WHERE MediaID NOT IN (SELECT MediaID FROM CheckoutLog WHERE ReturnDate IS NULL) AND IsArchived = 0")
            .ToList();
        }

        public bool IsAvaliable(int mediaID)
        {
            var selectedMedia = _dbContext.Media.FirstOrDefault(m => m.MediaID == mediaID);

            var checkOutLogMedia = _dbContext.CheckOutLog.ToList();

            if (selectedMedia!.IsArchived)
            {
                return false;
            }
            foreach(var log in checkOutLogMedia)
            {
                if(log.MediaID == selectedMedia!.MediaID)
                {
                   if(log.ReturnDate == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Update(CheckOutLog Checkout)
        {
            _dbContext.Update(Checkout);
            _dbContext.SaveChanges();
        }

        public CheckOutLog GetByID(int checkOutID)
        {
            return _dbContext.CheckOutLog.FirstOrDefault(c => c.CheckOutLogID == checkOutID);
        }

        public void Delete(CheckOutLog Checkout)
        {
            _dbContext.CheckOutLog.Remove(Checkout);
            _dbContext.SaveChanges();   
        }
    }
}
