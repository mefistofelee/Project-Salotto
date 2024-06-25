using PoponGate.Connections;
using PoponGate.Model.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salotto.Infrastructure.Persistence.Repositories
{
    public class DynamicRepository
    {
        public DynamicRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }

        public QueryResult ExecuteSqlQuery(string query)
        {
            var connection = SQLServerConnection.Initialize(ConnectionString);
            return connection.ExecuteSqlQuery(query);
        }
    }
}
