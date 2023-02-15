using Newtonsoft.Json;
using System;
using System.Web.ModelBinding;

namespace UCOMProject.Models
{
    public class HolidayModel
    {
        private int seniority { get { return DateTime.Now.Subtract(_startDate).Days / 365; } }
        private int _totalDays;
        private DateTime _startDate;
        public HolidayModel(DateTime startDate)
        {
            _startDate = startDate;
        }
        public int Id { get; set; }
        public int UsedDays { get; set; }
        public int TotalDays
        {
            get
            {
                if (TitleType == HolidayType.特休)
                {
                    if (seniority < 1) return _totalDays;
                    if (seniority < 2) return _totalDays + 3;
                    if (seniority < 3) return _totalDays + 7;
                    if (seniority < 5) return _totalDays + 11;
                    if (seniority < 10) return _totalDays + 12;
                    if (seniority >= 24) return _totalDays + 27;
                    return _totalDays + 13 + seniority - 10;
                }
                return _totalDays;
            }
            set
            {
                _totalDays = value;
            }
        }
        public HolidayType TitleType { get; set; }
        public string Title { get { return TitleType.ToString(); } set { } }
        public int Type { get; set; }
        [BindNever]
        public int CanUseDays
        {
            get
            {
                return TotalDays - UsedDays;
            }
        }
    }
}