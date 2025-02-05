using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;

namespace LibraryManagement.Data.Respository.Dapper
{
    public class DapperBorrowerRepository : IBorrowerRepository
    {
        public void Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Borrower? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }
    }
}
