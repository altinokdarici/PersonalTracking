using Fitbit.Api;
using PersonalTracking.StorageRepository.Clients;
using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.WebJob.Engine
{
    public class FitbitEngine : IEngine
    {
        const string ConsumerKey = "CONSUMER_KEY";
        const string ConsumerSecret = "CONSUMER_SECRET";
        const string AccessTokenKey = "ACCESSTOKEN_KEY";
        const string AccessTokenSecret = "ACCESSTOKEN_SECRET";

        public void Start()
        {
            DailyLogClient logClient = new DailyLogClient();

            FitbitClient fitbitClient = new FitbitClient(ConsumerKey, ConsumerSecret, AccessTokenKey, AccessTokenSecret);
            //for (DateTime dt = fitbitClient.GetActivityTrackerFirstDay().Value; dt <= DateTime.Today; dt = dt.AddDays(1))
            //{


            DateTime queryDate = DateTime.Today;

            var day = fitbitClient.GetDayActivity(queryDate).Summary;
            var sleep = fitbitClient.GetSleep(queryDate).Summary;

            DailyLog log = logClient.Get(queryDate);

            log.Distance = day.Distances.FirstOrDefault(x => x.Activity == "total").Distance;
            log.CaloriesOut = day.CaloriesOut;
            log.FairlyActiveMinutes = day.FairlyActiveMinutes;
            log.LightlyActiveMinutes = day.LightlyActiveMinutes;
            log.SedentaryMinutes = day.SedentaryMinutes;
            log.Steps = day.Steps;
            log.VeryActiveMinutes = day.VeryActiveMinutes;
            log.TimeInBadMinutes = sleep.TotalTimeInBed;
            log.SleepMinutes = sleep.TotalMinutesAsleep;
            logClient.InsertOrReplace(log);
            //}
        }
    }
}
