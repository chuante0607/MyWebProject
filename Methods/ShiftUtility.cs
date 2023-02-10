using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public static class ShiftUtility
    {
        /// <summary>
        /// 取得A.B班的當年度的工作天(做2休2)
        /// </summary>
        public static List<List<ShiftWorkModel>> getWorkDayOfYearByMonth(int year)
        {
            //to do:計算做2休2的週期
            const int workCycle = 4;
            List<List<ShiftWorkModel>> workDayByMonth = new List<List<ShiftWorkModel>>();
            for (int month = 1; month <= 12; month++)
            {
                List<ShiftWorkModel> workDays = new List<ShiftWorkModel>();
                //當年度的每個月有幾天
                int daysInMonth = DateTime.DaysInMonth(year, month);
                int day = 1;
                while (day <= daysInMonth)
                {
                    //currentDate屬於目前年份的第幾天
                    DateTime currentDate = new DateTime(year, month, day);
                    //round判斷哪幾天屬於A班或B班上班日
                    int round = currentDate.DayOfYear % workCycle;
                    ShiftType type = ShiftType.A;
                    if (round == 0)
                        round = workCycle;
                    //round
                    if (round > (workCycle / 2))
                        type = ShiftType.B;
                    workDays.Add(new ShiftWorkModel(type, currentDate));
                    day++;
                }
                workDayByMonth.Add(workDays);
            }
            return workDayByMonth;
        }
    }
}