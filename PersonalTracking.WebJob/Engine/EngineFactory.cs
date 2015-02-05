using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.WebJob.Engine
{
    public class EngineFactory
    {
        public static IEnumerable<IEngine> Get()
        {
            Type desiredType = typeof(IEngine);
            return AppDomain
                   .CurrentDomain
                   .GetAssemblies()
                   .SelectMany(assembly => assembly.GetTypes())
                   .Where(type => desiredType.IsAssignableFrom(type) && type.IsClass)
                   .Select(x => (IEngine)Activator.CreateInstance(x, false));
        }
    }
}
