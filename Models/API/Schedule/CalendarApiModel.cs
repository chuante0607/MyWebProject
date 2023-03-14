using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.API
{
    public class CalendarApiModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string classNames { get; set; }
        public string color { get; set; }
        public string remark { get; set; }
        public bool allDay { get { return true; } }
    }
}