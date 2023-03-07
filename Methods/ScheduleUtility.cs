using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                        List<List<ShiftViewModel>> shifts = HolidayUtility.GetWorkDayOfYearByMonth(empShift, DateTime.Now.Year);
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
    }
}