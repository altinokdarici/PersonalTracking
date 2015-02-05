using Microsoft.WindowsAzure.Storage.Table;
using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Clients
{
    public class DailyLogClient : TableClient<DailyLog>
    {

        public DailyLog Get(DateTime date)
        {
            string rowKey = date.ToString("yyyyMMdd");
            string partitionKey = date.ToString("yyyyMM");

            return base.Get(rowKey, partitionKey);
        }
        public IEnumerable<DailyLog> Get(int month, int year)
        {
            DateTime dt = new DateTime(year, month, 1);
            DailyLog log = new DailyLog(dt);
            TableQuery<DailyLog> query = new TableQuery<DailyLog>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, log.PartitionKey));

            return Table.ExecuteQuery(query);
        }
    }

}
