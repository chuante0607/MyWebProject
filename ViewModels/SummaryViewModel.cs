using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.API;

namespace UCOMProject.Models
{
    public class SummaryViewModel
    {
        public Employee Employee { get; set; }
        public List<HolidayViewModel> Holidays { get; set; }
        public List<ChartViewModel> ChartInfos { get; set; }
        public List<ScheduleNumApiModel> Attendance { get; set; }
    }
}