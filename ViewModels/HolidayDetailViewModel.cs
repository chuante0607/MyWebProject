using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using UCOMProject.Extension;

namespace UCOMProject.Models
{
    public class HolidayDetailViewModel
    {
        public int Id { get; set; }
        public string EId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Shift { get; set; }
        public int HId { get; set; }
        public string Title { get; set; }
        public HolidayType TitleType { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int State { get; set; }
        public string Remark { get; set; }
        public int UsedDays { get; set; }
        public string Prove { get; set; }
        public EmployeeViewModel Head { get;set; }
        public List<string> ProveImg
        {
            get
            {
                if (Prove != null)
                    return Prove.Split(',').ToList();
                else
                    return null;
            }
        }
    }
}