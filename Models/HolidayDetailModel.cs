using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class HolidayDetailModel 
    {
        public int Id { get; set; }
        public string EId { get; set; }
        public int HId { get; set; }
        public double UsedHours { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool State { get; set; }
        public string Remark { get; set; }
        public double UsedDays { get; set; }
    }
}