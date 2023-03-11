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
        /// 取得Plans
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Plan>> GetPlans()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                return await db.Plans.ToListAsync();
            }
        }

        /// <summary>
        /// 新增與更新Plans人力計畫
        /// </summary>
        /// <param name="plans"></param>
        /// <returns></returns>
        public static async Task<ApplyResult> HandlePlans(List<Plan> plans)
        {
            try
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    List<Plan> data = await db.Plans.ToListAsync();
                    foreach (Plan p in plans)
                    {
                        var hasData = data.FirstOrDefault(f => f.Id == p.Id);
                        if (hasData == null)
                        {
                            //沒存在就新增plan
                            db.Plans.Add(new Plan
                            {
                                Id = p.Id,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                PlanTitle = p.PlanTitle
                            });
                        }
                        else
                        {
                            //有存在就更新plan
                            hasData.StartDate = p.StartDate;
                            hasData.EndDate = p.EndDate;
                            hasData.PlanTitle = p.PlanTitle;
                        }
                    }

                    await db.SaveChangesAsync();
                }
                return new ApplyResult { isPass = true, msg = "資料更新成功" };
            }
            catch (Exception ex)
            {
                return new ApplyResult { isPass = false, msg = ex.Message };
            }
        }

        /// <summary>
        /// 刪除Plan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ApplyResult> DeletePlan(Guid id)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                try
                {
                    var plan = await db.Plans.FindAsync(id);
                    if (plan != null)
                    {
                        db.Plans.Remove(plan);
                        await db.SaveChangesAsync();
                    }
                    return new ApplyResult { isPass = true, msg = "刪除資料成功" };
                }
                catch (Exception ex)
                {
                    return new ApplyResult { isPass = false, msg = ex.Message };
                }

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