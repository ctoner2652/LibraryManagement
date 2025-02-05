using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repository
{
    public interface IBorrowerRepository
    {
        void Add(Borrower borrower);
        void Update(Borrower borrower);
        List<Borrower> GetAll();
        Borrower? GetById(int id);
        Borrower? GetByEmail(string email);
        string? DeleteUserByEmail(string email); 
    }
}
