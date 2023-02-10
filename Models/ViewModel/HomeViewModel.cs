using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class HomeViewModel
    {
        public EmployeeModel Employee { get; set; }
        public List<HolidayModel> Holidays { get; set; }
        public Dictionary<int, List<HolidayModel.Chart>> ChartDict { get; set; }
    }
}