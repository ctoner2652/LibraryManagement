using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Tests.Mocks;
using NUnit.Framework;

namespace LibraryManagement.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestDuplicateUpdateOrCreate()
        {
            IBorrowerService borrowerService = new BorrowerService(new TestBorrowerRepository());

            Borrower test = new Borrower();
            test.FirstName = "Toner";
            test.LastName = "Colby";
            test.Email = "test@test.com";

            var result = borrowerService.UpdateBorrower(test);
            var result2 = borrowerService.AddBorrower(test);

            Assert.That(result.Ok, Is.False);
            Assert.That(result2.Ok, Is.False);
        }


    }
}
