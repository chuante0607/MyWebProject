using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Extension
{
    public static class EnumExtension
    {
        public static HolidayType xTranHolidayEnum(this string title)
        {
            return (HolidayType)Enum.Parse(typeof(HolidayType), title);
        }

        public static ShiftType xTranShiftEnum(this string shift)
        {
            return (ShiftType)Enum.Parse(typeof(ShiftType), shift);
        }

        public static BranchType xTranBranchEnum(this string branch)
        {
            return (BranchType)Enum.Parse(typeof(BranchType), branch);
        }

        public static JobTitleType xTranJobTitleEnum(this string title)
        {
            return (JobTitleType)Enum.Parse(typeof(JobTitleType), title);
        }

        public static bool xTranSexBool(this string sex)
        {
            return sex == "男" ? true : false;
        }
    }
}