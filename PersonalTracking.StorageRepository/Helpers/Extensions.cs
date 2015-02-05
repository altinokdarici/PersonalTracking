using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Helpers
{
    public static class Extensions
    {
        public static DateTime ToDate(this double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
        public static DateTime ToDate(this int unixTimeStamp)
        {
            return ((double)unixTimeStamp).ToDate();
        }
    }
}
