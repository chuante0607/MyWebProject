using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using UCOMProject.Extension;

namespace UCOMProject.Models
{
    public class ApplyResult
    {
        public bool Error { get; set; }
        public string Msg { get; set; }
        public List<string> FilesNames { get; set; }
        public string FileName { get; set; }
    }

    public class ApplyViewModel
    {
        public Employee Employee { get; set; }
        public List<HolidayViewModel> Holidays { get; set; }
        public List<List<ShiftWork>> WorkDayOfYearByMonth { get; set; }

        [Required]
        public string EId { get; set; }

        [Required]
        public int HId { get; set; }

        [Required]
        public bool ProveType { get; set; }

        [Required]
        public string Title { get; set; }

        [BindNever]
        public HolidayType TitleType { get; set; }

        [Required]
        [Range(0, 999)]
        public int UsedDays { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [BindNever]
        public bool? Allow { get; set; }

        [Required]
        public string Remark { get; set; }

        [BindNever]
        public List<DateTime> RangeDate
        {
            get
            {
                List<DateTime> r = new List<DateTime>();
                if (this.RangeDateString.Count() > 0)
                {
                    r = this.RangeDateString.Select(s => s.ToDatetime()).ToList();
                }

                return r;
            }
        }

        [Required]
        public List<string> RangeDateString { get; set; } = new List<string>();

        /// <summary>
        /// 接收多筆檔案
        /// </summary>
        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        //public List<string> Prove { get; set; }


    }
}