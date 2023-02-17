using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using UCOMProject.Extension;

namespace UCOMProject.Models
{
    public class HolidayViewModel
    {
        private int _seniority { get { return DateTime.Now.Subtract(_startDate).Days / 365; } }
        private int _sourceDays;
        private DateTime _startDate;
        public HolidayViewModel(DateTime startDate)
        {
            _startDate = startDate;
        }
        public int HId { get; set; }
        public string Title { get; set; }
        public HolidayType TitleType { get { return Title.xTranEnum(); } }
        public int UsedDays { get; set; }
        public int TotalDays
        {
            get
            {
                if (TitleType == HolidayType.特休)
                {
                    if (_seniority < 1) return _sourceDays;
                    if (_seniority < 2) return _sourceDays + 3;
                    if (_seniority < 3) return _sourceDays + 7;
                    if (_seniority < 5) return _sourceDays + 11;
                    if (_seniority < 10) return _sourceDays + 12;
                    if (_seniority >= 24) return _sourceDays + 27;
                    return _sourceDays + 13 + _seniority - 10;
                }
                return _sourceDays;
            }
            set
            {
                _sourceDays = value;
            }
        }
        public int CanUseDays
        {
            get
            {
                return TotalDays - UsedDays;
            }
        }
    }
}