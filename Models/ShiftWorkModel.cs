using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ShiftWorkModel
    {
        public ShiftWorkModel(ShiftType type, DateTime work)
        {
            this.type = type;
            this.work = work;
        }
        public string shiftType { get { return this.type.ToString(); } }
        private ShiftType type { get; set; }
        public DateTime workDate { get { return this.work; } }
        private DateTime work { get; set; }
    }
}