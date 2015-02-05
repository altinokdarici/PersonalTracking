using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Models
{
    public class DailyLog : TableEntity
    {
        public DailyLog()
        {

        }

        public DailyLog(DateTime date)
        {
            RowKey = date.ToString("yyyyMMdd");
            PartitionKey = date.ToString("yyyyMM");
        }

        public DateTime Date
        {
            get
            {
                return DateTime.ParseExact(RowKey, "yyyyMMdd", null);
            }
        }

        public int CaloriesOut { get; set; }

        public double FairlyActiveMinutes { get; set; }
        public TimeSpan FairlyActive
        {
            get
            {
                return TimeSpan.FromMinutes(FairlyActiveMinutes);
            }
        }

        public double LightlyActiveMinutes { get; set; }
        public TimeSpan LightlyActive
        {
            get
            {
                return TimeSpan.FromMinutes(LightlyActiveMinutes);
            }
        }
        public double Distance { get; set; }
        public double SedentaryMinutes { get; set; }
        public TimeSpan Sedentary
        {
            get
            {
                return TimeSpan.FromMinutes(SedentaryMinutes);
            }
        }

        public int Steps { get; set; }

        public double VeryActiveMinutes { get; set; }
        public TimeSpan VeryActive
        {
            get
            {
                return TimeSpan.FromMinutes(VeryActiveMinutes);
            }
        }

        public int SleepMinutes { get; set; }
        public TimeSpan Sleep
        {
            get
            {
                return TimeSpan.FromMinutes(SleepMinutes);
            }
        }

        public int TimeInBadMinutes { get; set; }
        public TimeSpan TimeInBad
        {
            get
            {
                return TimeSpan.FromMinutes(TimeInBadMinutes);
            }
        }
    }
}
