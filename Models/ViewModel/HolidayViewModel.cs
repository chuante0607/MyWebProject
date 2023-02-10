using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class HolidayViewModel
    {
        public EmployeeModel employee { get; set; }
        public List<HolidayModel> Holidays { get; set; }
        public List<List<ShiftWorkModel>> WorkDayOfYearByMonth { get; set; }
    }
}