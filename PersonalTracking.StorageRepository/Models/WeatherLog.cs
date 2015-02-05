using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalTracking.StorageRepository.Helpers;
namespace PersonalTracking.StorageRepository.Models
{
    public class WeatherLog : TableEntity
    {
        public WeatherLog()
        {

        }
        public WeatherLog(DateTime date)
        {
            RowKey = date.ToString("yyyyMMddhhmm");
            PartitionKey = date.ToString("yyyyMM");
        }
        public DateTime Date
        {
            get
            {
                return DateTime.ParseExact(RowKey, "yyyyMMddhhmm", null);
            }
        }
        public int CityId { get; set; }
        public int Sunrise { get; set; }
        public DateTime SunriseAt
        {
            get
            {
                return Sunrise.ToDate();
            }
        }
        public int Sunset { get; set; }
        public DateTime SunsetAt
        {
            get
            {
                return Sunset.ToDate();
            }
        }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public int Humidty { get; set; }
        public double WindSpeed { get; set; }
        public double WindDegree { get; set; }
        public double Rain { get; set; }
        public int Clouds { get; set; }


    }
}
