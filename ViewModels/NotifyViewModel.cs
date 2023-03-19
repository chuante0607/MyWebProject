using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class NotifyViewModel
    {
        public DateTime Date { get; set; }
        public ShiftType Shift { get; set; }
        public int NeedNum { get; set; }
    }
}