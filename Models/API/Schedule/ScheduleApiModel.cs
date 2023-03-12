using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.API
{
    public class ScheduleApiModel
    {
        public List<CalendarApiModel> calendars { get; set; }
        public List<List<ShiftViewModel>> shifts { get; set; }
        public Dictionary<DateTime, int> leaveNums { get; set; }
        public List<Plan> plans { get; set; }
        public List<EmployeeViewModel> employees { get; set; }
        public Dictionary<DateTime, int> leaveCount { get; set; }
    }
}