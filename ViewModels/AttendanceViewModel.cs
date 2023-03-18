using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.API;

namespace UCOMProject.Models
{
    public class AttendanceViewModel
    {
        public EmployeeViewModel employee { get; set; }
        public List<Attendance> schedules { get; set; }

    }
}