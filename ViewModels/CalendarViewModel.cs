using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class CalendarViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ClassNames { get; set; }
        public string Color { get; set; }
        //public string TextColor { get; set; }
        //public bool AllDay { get; set; }
    }
}