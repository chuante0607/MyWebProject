using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.API;

namespace UCOMProject.Models
{
    public class AttendanceViewModel
    {
        public DateTime ViewDate { get; set; }
        public string EId { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Image { get; set; }
        public ShiftType ShiftType { get; set; }
        public string Shift { get { return ShiftType.ToString(); } set { } }
        public BranchType BranchType { get; set; }
        public string Branch { get { return BranchType.ToString(); } set { } }
        public int BranchId { get { return (int)BranchType; } set { } }
        public JobTitleType JobType { get; set; }
        public int JobRank { get; set; }
        public string JobTitle { get { return JobType.ToString(); } set { } }
        public bool IsLeave { get; set; }

        public int Id { get; set; }
        public int HId { get; set; }
        public string Title { get; set; }
        public HolidayType TitleType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DateTime> RangDate { get; set; }
        public int State { get; set; }
        public string Remark { get; set; }
    }
}