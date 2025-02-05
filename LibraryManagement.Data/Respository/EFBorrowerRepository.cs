using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;

namespace LibraryManagement.Data.Respository
{
    public class EFBorrowerRepository : IBorrowerRepository
    {
        private LibraryContext _dbContext;

        public EFBorrowerRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }
        public void Add(Borrower borrower)
        {
            _dbContext.Borrower.Add(borrower);
            _dbContext.SaveChanges();
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetById(int id)
        {
            var borrower = _dbContext.Borrower.FirstOrDefault(e => e.BorrowerID == id);
            return borrower;
        }
        
        public Borrower? GetByEmail(string email)
        {
            var borrower = _dbContext.Borrower.FirstOrDefault(e => e.Email == email);
            return borrower;
        }

        public void Update(Borrower borrower)
        {
            _dbContext.Borrower.Update(borrower);
            _dbContext.SaveChanges();
        }

        public string? DeleteUserByEmail(string email)
        {
            var borrowerToRemove = _dbContext.Borrower.FirstOrDefault(e => e.Email == email);
            string? name;
            if (borrowerToRemove != null)
            {
                name = borrowerToRemove.FirstName + " " + borrowerToRemove.LastName;
                _dbContext.Borrower.Remove(borrowerToRemove);
                _dbContext.SaveChanges();
            }
            else
            {
                name = null;
            }

            return name;

        }
    }
}
