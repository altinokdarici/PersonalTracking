using PersonalTracking.StorageRepository.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalTracking.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DailyLogClient client = new DailyLogClient();
            var model = client.Get(DateTime.Now.Month, DateTime.Now.Year).ToList();
            DateTime preDate = DateTime.Today.AddMonths(-1);
            model.AddRange(client.Get(preDate.Month, preDate.Year));
            return View(model.OrderByDescending(x => x.Date));
        }

        public ActionResult RescueTime()
        {
            RescueTimeLogClient client = new RescueTimeLogClient();
            var model = client.Get(DateTime.Today.Year);
            return View(model);
        }

        public ActionResult Weather()
        {
            WeatherLogClient client = new WeatherLogClient();
            var model = client.Get(DateTime.Today.Month, DateTime.Today.Year);
            return View(model);
        }
    }
}