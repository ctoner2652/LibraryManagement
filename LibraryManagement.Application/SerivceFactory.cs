using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Interfaces.Application;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Data.Respository;

namespace LibraryManagement.Application
{
    public class SerivceFactory
    {
        private IAppConfiguration _config;

        public SerivceFactory(IAppConfiguration config)
        {
            _config = config;
        }

        public IBorrowerService CreateBorrowerService()
        {
            return new BorrowerService(
                new EFBorrowerRepository(_config.GetConnectionString()));
        }

        public IMediaService CreateMediaService()
        {
            return new MediaService(
                new EFMediaRepository(_config.GetConnectionString()));
        }
        public ICheckOutLog CreateCheckOutService()
        {
            return new CheckOutLogService(
                new EFCheckoutLogRespository(_config.GetConnectionString()), new EFBorrowerRepository(_config.GetConnectionString()));
        }
    }
}
