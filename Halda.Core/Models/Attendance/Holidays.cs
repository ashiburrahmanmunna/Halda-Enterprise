using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Attendance
{
    public class Holidays : BaseModel
    {
        public string HolidaysName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public int Day { get; set; }
        public string HolidayType { get; set; } = string.Empty;
    }

}
