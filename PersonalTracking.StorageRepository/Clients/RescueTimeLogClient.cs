using Microsoft.WindowsAzure.Storage.Table;
using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Clients
{
    public class RescueTimeLogClient : TableClient<RescueTimeLog>
    {
        public RescueTimeLog Get(DateTime date)
        {
            string rowKey = date.ToString("yyyyMMdd");
            string partitionKey = date.Year.ToString();

            return base.Get(rowKey, partitionKey);
        }
        public IEnumerable<RescueTimeLog> Get(int year)
        {
            TableQuery<RescueTimeLog> query = new TableQuery<RescueTimeLog>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, year.ToString()));

            return Table.ExecuteQuery(query);
        }
    }
}
