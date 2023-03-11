using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Extension;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public static class ScheduleUtility
    {
        /// <summary>
        /// 設定AB的排班表
        /// </summary>
        public static void SetSchedule()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                int month = DateTime.Now.Month;
                List<Employee> employees = db.Employees.ToList();
                foreach (Employee emp in employees)
                {
                    ShiftType empShift = emp.Shift.xTranShiftEnum();
                    if (empShift == ShiftType.A班 || empShift == ShiftType.B班)
                    {
                        List<List<ShiftViewModel>> shifts = GetWorkDayOfYearByMonth(empShift, DateTime.Now.Year);
                        List<ShiftViewModel> result = shifts[month - 1];
                        List<Schedule> schedules = new List<Schedule>();
                        foreach (ShiftViewModel shift in result)
                        {
                            if (shift.IsWork)
                            {
                                Schedule schedule = new Schedule();
                                schedule.EId = emp.EId;
                                schedule.WorkDay = shift.CheckDate;
                                schedules.Add(schedule);
                            }
                        }
                        db.Schedules.AddRange(schedules);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static async Task<List<Schedule>> GetSchedulesByEmp(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                return await db.Schedules.Where(w => w.EId == eid).ToListAsync();
            }
        }



        /// <summary>
        /// 取得A.B班的當年度的工作天(做2休2)
        /// </summary>
        public static List<List<ShiftViewModel>> GetWorkDayOfYearByMonth(ShiftType shift, int year)
        {
            const int WorkCycle = 4;
            List<List<ShiftViewModel>> workDayByMonth = new List<List<ShiftViewModel>>();
            //to do:
            //計算做2休2的週期
            bool isWork = false;
            for (int month = 1; month <= 12; month++)
            {
                List<ShiftViewModel> workDays = new List<ShiftViewModel>();
                //當年度的每個月有幾天
                int daysInMonth = DateTime.DaysInMonth(year, month);
                int day = 1;
                while (day <= daysInMonth)
                {
                    //currentDate屬於目前年份的第幾天
                    DateTime currentDate = new DateTime(year, month, day);
                    //round判斷哪幾天屬於A班或B班上班日
                    int round = currentDate.DayOfYear % WorkCycle;
                    switch (shift)
                    {
                        case ShiftType.A班:
                            isWork = round == 1 || round == 2 ? true : false;
                            break;
                        case ShiftType.B班:
                            isWork = round == 0 || round == 3 ? true : false;
                            break;
                        default:
                            break;
                    }
                    workDays.Add(new ShiftViewModel(shift, currentDate, isWork));
                    day++;
                }
                workDayByMonth.Add(workDays);
            }
            return workDayByMonth;
        }

        /// <summary>
        /// 取得常日班的工作天
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<List<ShiftViewModel>> GetWorkDayOfYearByMonth(string[] file, int year)
        {
            List<List<ShiftViewModel>> workDayByMonth = new List<List<ShiftViewModel>>();
            List<ShiftViewModel> ShiftViewModels = new List<ShiftViewModel>();
            foreach (var line in file)
            {
                List<string> str = line.Split(',').ToList();
                if (DateTime.TryParseExact(str[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    if (str[2] == "2")
                        ShiftViewModels.Add(new ShiftViewModel(ShiftType.常日班, date, false));
                    else
                        ShiftViewModels.Add(new ShiftViewModel(ShiftType.常日班, date, true));
                }
            }
            workDayByMonth = ShiftViewModels.GroupBy(g => g.CheckDate.Month).Select(s => s.ToList()).ToList();
            return workDayByMonth;
        }

    }
}