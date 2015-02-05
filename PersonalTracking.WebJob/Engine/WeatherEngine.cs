using PersonalTracking.StorageRepository.Clients;
using PersonalTracking.StorageRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.WebJob.Engine
{
    public class WeatherEngine : IEngine
    {
        public void Start()
        {
            HttpClient client = new HttpClient();
            string json = client.GetStringAsync("http://api.openweathermap.org/data/2.5/weather?id=146286&units=metric").Result;
            Rootobject obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(json);
            WeatherLogClient logClient = new WeatherLogClient();
            WeatherLog log = new WeatherLog(DateTime.Now)
            {
                Clouds = obj.clouds.all,
                Description = obj.weather.FirstOrDefault().description,
                Humidty = obj.main.humidity,
                Pressure = obj.main.pressure,
                Rain = obj.rain._3h,
                Sunrise = obj.sys.sunrise,
                Sunset = obj.sys.sunset,
                Temperature = obj.main.temp,
                WindDegree = obj.wind.deg,
                WindSpeed = obj.wind.speed,
                CityId = obj.id
            };
            logClient.InsertOrReplace(log);

        }

        #region JsonClasses

        public class Rootobject
        {
            public Coord coord { get; set; }
            public Sys sys { get; set; }
            public Weather[] weather { get; set; }
            public string _base { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Rain rain { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public class Coord
        {
            public float lon { get; set; }
            public float lat { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public float message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Main
        {
            public float temp { get; set; }
            public float temp_min { get; set; }
            public float temp_max { get; set; }
            public float pressure { get; set; }
            public int humidity { get; set; }
        }

        public class Wind
        {
            public float speed { get; set; }
            public float deg { get; set; }
        }

        public class Rain
        {
            public int _3h { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        #endregion

    }
}
