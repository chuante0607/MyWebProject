using System;
using System.Collections.Generic;

namespace UCOMProject.Models
{
    public class ShiftViewModel
    {
        public string Shift { get; set; }
        public DateTime CheckDate { get; set; }
        public bool IsWork { get; set; }
        public ShiftViewModel(ShiftType shift, DateTime data, bool isWork)
        {
            Shift = shift.ToString();
            CheckDate = data;
            IsWork = isWork;
        }
    }
}