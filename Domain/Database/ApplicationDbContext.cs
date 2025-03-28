using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection(string connectionString = "DefaultConnection")
        {
            var path = Directory.GetCurrentDirectory();
            string? DbPath = "Filename=" + Path.Join(path, _configuration.GetConnectionString(connectionString));

            return new SqliteConnection(DbPath);

        }
    }
}
