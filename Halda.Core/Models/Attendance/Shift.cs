using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Attendance
{
    public class Shift : BaseModel
    {
       
        public string ShiftName { get; set; } = string.Empty;
        public string ShiftType { get; set; } = string.Empty;
        public TimeSpan ShiftIn { get; set; }  
        public TimeSpan ShiftOut { get; set; }  
        public TimeSpan? BreakIn { get; set; }  
        public TimeSpan? BreakOut { get; set; }  
        public TimeSpan? SecondBreakIn { get; set; } 
        public TimeSpan? SecondBreakOut { get; set; } 
        public TimeSpan? ThirdBreakIn { get; set; } 
        public TimeSpan? ThirdBreakOut { get; set; } 
        public TimeSpan? NightCross { get; set; }
        public TimeSpan? WeekendAllowTime { get; set; } 
        public TimeSpan? TotalHour { get; set; }  
    }

}
