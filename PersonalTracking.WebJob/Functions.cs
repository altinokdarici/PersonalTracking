using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace PersonalTracking.WebJob
{
    public class Functions
    {
        // This function will be triggered based on the schedule you have set for this WebJob
        [NoAutomaticTrigger]
        public static void Log()
        {
            Engine.EngineFactory.Get().ToList().ForEach(x => x.Start());
        }
    }
}
