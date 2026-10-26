using LibraryProject.Core.Common;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace LibraryProject.Infra.Common
{
    public class DbContext : IDbContext
    {
        private DbConnection? connection;
        private readonly IConfiguration configuration;

        public DbContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public DbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new OracleConnection(configuration["ConnectionStrings:DBConnectionString"]);
                    connection.Open();
                }
                else if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            }
        }
    }
}
