using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Models
{
    public class RescueTimeLog : TableEntity
    {
        public RescueTimeLog()
        {

        }
        public RescueTimeLog(DateTime date)
        {
            this.RowKey = date.ToString("yyyyMMdd");
            this.PartitionKey = date.Year.ToString();
        }
        public DateTime Date
        {
            get
            {
                return DateTime.ParseExact(RowKey, "yyyyMMdd", null);
            }
        }
        public int ProductivityPulse { get; set; }
        public double ProductivePercentage { get; set; }
        public double DistractingPercentage { get; set; }
        public double NeutralPercentage { get; set; }
        public double TotalHours { get; set; }
        public TimeSpan Total
        {
            get
            {
                return TimeSpan.FromHours(TotalHours);
            }
        }
        public double ProductiveHours { get; set; }
        public TimeSpan Productive
        {
            get
            {
                return TimeSpan.FromHours(ProductiveHours);
            }
        }
        public double DistractingHours { get; set; }
        public TimeSpan Distracting
        {
            get
            {
                return TimeSpan.FromHours(DistractingHours);
            }
        }
        public double NeutralHours { get; set; }
        public TimeSpan Neutral
        {
            get
            {
                return TimeSpan.FromHours(NeutralHours);
            }
        }

    }
}
