using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class EmployeeViewModel
    {
        public string EId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Branch { get; set; }
        public string JobTitle { get; set; }
        public int JobRank { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EnglishName { get; set; }
        public string Shift { get; set; }
        public DateTime StartDate { get; set; }
    }
}