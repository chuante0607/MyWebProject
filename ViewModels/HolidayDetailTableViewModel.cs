using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class HolidayDetailTableViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public List<HolidayDetailViewModel> Details { get; set; }
    }
}