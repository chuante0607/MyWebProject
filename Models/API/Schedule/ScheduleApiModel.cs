using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.API
{
    public class ScheduleApiModel
    {
        public List<Plan> plans { get; set; }
        public List<CalendarApiModel> calendars { get; set; }
        public List<List<ShiftViewModel>> shifts { get; set; }
        public List<EmployeeViewModel> employees { get; set; }
        public List<ScheduleNumApiModel> attendance { get; set; }
    }

    public class ScheduleNumApiModel
    {
        public DateTime date { get; set; }
        public int planNum { get; set; }
        public int shouldNum { get; set; }
        public int leaveNum { get; set; }
        public int realNum
        {
            get
            {
                return (shouldNum - leaveNum) - planNum;
            }
        }
    }

}