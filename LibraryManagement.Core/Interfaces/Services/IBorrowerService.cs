using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        Result<List<Borrower>> GetAllBorrowers();
        Result<Borrower> GetBorrower(string email);
        Result AddBorrower(Borrower newBorrower);
        Result<Borrower> UpdateBorrower(Borrower borrowerToUpdate);
        Result<string> RemoveBorrower(string email);
        Result<Borrower> GetByID(int ID);
    }
}
