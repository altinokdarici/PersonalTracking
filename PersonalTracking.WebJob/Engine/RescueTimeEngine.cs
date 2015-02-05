using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PersonalTracking.StorageRepository.Helpers;
namespace PersonalTracking.WebJob.Engine
{
    public class RescueTimeEngine : IEngine
    {
        public void Start()
        {
            StorageRepository.Clients.RescueTimeLogClient logClient = new StorageRepository.Clients.RescueTimeLogClient();

            HttpClient client = new HttpClient();
            string json = client.GetStringAsync("https://www.rescuetime.com/anapi/daily_summary_feed?key=YOUR_KEY").Result;
            dynamic result = ((dynamic)(Newtonsoft.Json.JsonConvert.DeserializeObject(json)))[0];
            DateTime dtDateTime = ((int)result.id.Value).ToDate();
            RescueTimeLog log = logClient.Get(dtDateTime.ToString("yyyyMMdd"), dtDateTime.Year.ToString());

            log.ProductivityPulse = (int)result.productivity_pulse.Value;
            log.ProductivePercentage = (double)result.all_productive_percentage.Value;
            log.DistractingPercentage = (double)result.all_distracting_percentage.Value;
            log.NeutralPercentage = (double)result.neutral_percentage.Value;
            log.TotalHours = (double)result.total_hours.Value;
            log.ProductiveHours = (double)result.all_productive_hours.Value;
            log.DistractingHours = (double)result.all_distracting_hours.Value;
            log.NeutralHours = (double)result.neutral_hours.Value;
            logClient.InsertOrReplace(log);

        }
    }
}
