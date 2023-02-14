using System.Collections.Generic;

namespace UCOMProject.Models
{
    public class ShiftViewModel
    {
        public EmployeeModel employee { get; set; }
        public List<List<ShiftWorkModel>> WorkDayOfYearByMonth { get; set; }
    }
}