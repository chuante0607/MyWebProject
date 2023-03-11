using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ScheduleViewModel
    {
        public List<CalendarViewModel> Calendars { get; set; }
        public List<List<ShiftViewModel>> Shifts { get; set; }
        public Dictionary<DateTime, int> LeaveNums { get; set; }
        public List<Plan> plans { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }
}