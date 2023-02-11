using System.Web.ModelBinding;

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

        public int Id { get; set; }
        public string Title { get; set; }
        public double TotalDays { get; set; }
        public int Type { get; set; }

        public int HId { get; set; }
        public double UsedDays { get; set; }

        [BindNever]
        public double CanUseDays
        {
            get
            {
                return TotalDays - UsedDays;
            }
        }
    }
}