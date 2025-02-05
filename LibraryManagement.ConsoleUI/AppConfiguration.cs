using LibraryManagement.Core;
using LibraryManagement.Core.Interfaces.Application;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.ConsoleUI
{
    public class AppConfiguration : IAppConfiguration
    {
        private IConfiguration _configuration;
        public AppConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
        }
        public string GetConnectionString()
        {
            return _configuration["LibraryDb"] ?? "";
        }

        public DatabaseMode GetDatabaseMode()
        {
            if (_configuration["DatabaseMode"] == "ORM")
            {
                return DatabaseMode.ORM;
            }
            else
            {
                return DatabaseMode.DirectSQL;
            }
        }
    }
}
