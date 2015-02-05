using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Models
{
    public class ResourceLog : TableEntity
    {
        public ResourceLog()
        {

        }
        public ResourceLog(DateTime dt)
        {
            StartDate = dt;
            RowKey = StartDate.ToString("yyyyMMddhhmm");
            PartitionKey = StartDate.ToString("yyyyMMdd");
        }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double CpuUsage { get; set; }
        public double FreeMemory { get; set; }

    }
}
