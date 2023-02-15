using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Extension
{
    public static class EnumExtension
    {
        public static HolidayType xTranEnum(this string title)
        {
            return (HolidayType)Enum.Parse(typeof(HolidayType), title);
        }
    }
}