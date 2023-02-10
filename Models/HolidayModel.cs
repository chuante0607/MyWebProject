using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class HolidayModel
    {
        public class Chart
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public double Days { get; set; }
        }

        public HolidayModel()
        {

        }
        private readonly int _workHours = 10;
        public int HId { get; set; }
        public string Title { get; set; }
        public double TotalDays { get; set; }
        public double UsedDays { get; set; }

        public double CanUseDays
        {
            get
            {
                return TotalDays - UsedDays;
            }
        }
        public double CanUseHours
        {
            get
            {
                return CanUseDays * _workHours;
            }
        }
        public double UsedHours
        {
            get
            {
                return UsedDays * _workHours;
            }
        }

        
    }

}