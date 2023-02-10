using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ShiftViewModel
    {
        public EmployeeModel employee { get; set; }
        public List<List<ShiftWorkModel>> WorkDayOfYearByMonth { get; set; }
    }
}