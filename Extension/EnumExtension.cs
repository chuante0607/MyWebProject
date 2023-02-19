using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Extension
{
    public static class EnumExtension
    {
        public static HolidayType xHolidayTranEnum(this string title)
        {
            return (HolidayType)Enum.Parse(typeof(HolidayType), title);
        }

        public static ShiftType xShiftTranEnum(this string shift)
        {
            return (ShiftType)Enum.Parse(typeof(ShiftType), shift);
        }
    }
}