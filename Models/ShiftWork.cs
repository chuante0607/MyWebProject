using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class ShiftWork
    {
        private DateTime _work { get; set; }
        private ShiftType _type { get; set; }

        public ShiftWork(ShiftType type, DateTime work)
        {
            _type = type;
            _work = work;
        }
        public string ShiftType { get { return _type.ToString(); } }
        public DateTime WorkDate { get { return _work; } }
    }
}