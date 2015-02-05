using Microsoft.WindowsAzure.Storage.Table;
using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Clients
{
    public class WeatherLogClient : TableClient<Models.WeatherLog>
    {
        public IEnumerable<Models.WeatherLog> Get(int month, int year)
        {

            DateTime dt = new DateTime(year, month, 1);
            string partitionKey = dt.ToString("yyyyMM");
            TableQuery<WeatherLog> query = new TableQuery<WeatherLog>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            return Table.ExecuteQuery(query);
        }
    }
}
