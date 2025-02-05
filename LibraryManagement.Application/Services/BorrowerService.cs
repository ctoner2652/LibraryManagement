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
    public class BorrowerService : IBorrowerService
    {
        private IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public Result AddBorrower(Borrower newBorrower)
        {
            try
            {
                var duplicate = _borrowerRepository.GetByEmail(newBorrower.Email);
                if (duplicate != null)
                {
                    return ResultFactory.Fail($"Borrower with email {newBorrower.Email} already exists!");
                }
                else
                {
                    _borrowerRepository.Add(newBorrower);
                    return ResultFactory.Success();
                }
            }
            catch (Exception ex)
            {
                {
                    return ResultFactory.Fail(ex.Message);
                }
            }
        }

        public Result<List<Borrower>> GetAllBorrowers()
        {
            try
            {
                var borrowers = _borrowerRepository.GetAll();
                return ResultFactory.Success(borrowers);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Borrower>>(ex.Message);
            }
        }

        public Result<Borrower> GetBorrower(string email)
        {
            try
            {
                var borrower = _borrowerRepository.GetByEmail(email);
                return borrower is null ?
                    ResultFactory.Fail<Borrower>($"Borrower with email: {email} not found!") :
                    ResultFactory.Success(borrower);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }

        public Result<Borrower> GetByID(int ID)
        {
            try
            {
                var borrower = _borrowerRepository.GetById(ID);
                return borrower is null ?
                    ResultFactory.Fail<Borrower>($"Borrower with id: {ID} not found!") :
                    ResultFactory.Success(borrower);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }

        public Result<string> RemoveBorrower(string email)
        {
            try
            {
                var name = _borrowerRepository.DeleteUserByEmail(email);
                return name is null ? ResultFactory.Fail<string>($"Borrower with email: {email} not found!") :
                                      ResultFactory.Success<string>(name);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<string>(ex.Message);
            }
        }

        public Result<Borrower> UpdateBorrower(Borrower borrowerToUpdate)
        {
            try
            {
                var duplicate = _borrowerRepository.GetByEmail(borrowerToUpdate.Email);
                if (duplicate != null && duplicate.BorrowerID != borrowerToUpdate.BorrowerID)
                {
                    return ResultFactory.Fail<Borrower>($"Borrower with email {borrowerToUpdate.Email} already exists!");
                }
                else
                {
                    _borrowerRepository.Update(borrowerToUpdate);
                    return ResultFactory.Success<Borrower>(borrowerToUpdate);
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }
    }
}
