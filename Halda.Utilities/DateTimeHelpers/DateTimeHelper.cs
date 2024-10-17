using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Utilities.DateTimeHelpers
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertUtcToLocal(DateTime utcDateTime)
        {
            // Get the local time zone information
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            // Convert the UTC time to local time
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);

            return localTime;
        }
        public static string UtcString(this DateTime utcDateTime)
        {

            return utcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}
