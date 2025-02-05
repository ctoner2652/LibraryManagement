using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repository;
using NUnit.Framework.Internal;

namespace LibraryManagement.Tests.Mocks
{
    public class TestBorrowerRepository : IBorrowerRepository
    {
        public void Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public string? DeleteUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            Borrower test = new Borrower();
            test.Email = "test@test.com";
            test.FirstName = "Colby";
            test.LastName = "Toner";
            return test;
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
