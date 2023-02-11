using System;
using System.Collections.Generic;
using System.Linq;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public static class HolidayUtility
    {
        /// <summary>
        /// 查詢員工可用休假的天數
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<HolidayModel> getCanUseHolidaysByEmpID(string id)
        {
            List<HolidayModel> holidayViewModels = new List<HolidayModel>();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.SingleOrDefault(s => s.EId == id);
                //員工休假資料
                //統計今年度已用及未用假別天數
                List<Holiday> holidays = db.Holidays.OrderBy(o => o.Id).ToList();
                foreach (Holiday item in holidays)
                {
                    HolidayModel holidayModel = new HolidayModel()
                    {
                        HId = item.Id,
                        Title = item.Title,
                        TotalDays = item.TotalDays,
                        //查詢已用的請假天數
                        UsedDays = emp.HolidayDetails.Where(ld => ld.HId == item.Id && ld.BeginDate.Year == DateTime.Now.Year).Select(s => s.UsedDays).Sum(),
                    };
                    holidayViewModels.Add(holidayModel);
                }
                return holidayViewModels;
            }
        }

        /// <summary>
        /// 查詢charts資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Dictionary<int, List<HolidayModel.Chart>> getchartDictByEmpID(string id)
        {
            Dictionary<int, List<HolidayModel.Chart>> chartsDict = new Dictionary<int, List<HolidayModel.Chart>>();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.SingleOrDefault(s => s.EId == id);
                //查詢指定年度的每月休假天數總計
                List<HolidayModel.Chart> chartGroup = emp.HolidayDetails.GroupBy(g => (g.BeginDate.Year, g.BeginDate.Month), g => g.UsedDays)
                .Select(s => new HolidayModel.Chart
                {
                    Year = s.Key.Year,
                    Month = s.Key.Month,
                    Days = s.Sum()
                }).OrderBy(o => o.Year).ThenBy(t => t.Month).ToList();

                //整合charts所需要的資訊
                foreach (int year in chartGroup.Select(s => s.Year).Distinct())
                {
                    List<HolidayModel.Chart> chartList = new List<HolidayModel.Chart>();
                    for (int month = 1; month <= 12; month++)
                    {
                        HolidayModel.Chart chartInfo = chartGroup.Find(x => x.Year == year && x.Month == month);
                        if (chartInfo == null)
                        {
                            chartList.Add(new HolidayModel.Chart() { Year = year, Month = month, Days = 0 });
                        }
                        else
                        {
                            chartList.Add(chartInfo);
                        }
                    }
                    chartsDict.Add(year, chartList);
                }
            }
            return chartsDict;
        }

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


        /// <summary>
        /// 查詢休假歷史紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static List<HolidayDetailViewModel> getHolidayDetailsByEmpID(string id)
        //{
        //    List<HolidayDetailViewModel> holidayDetailViewModels = new List<HolidayDetailViewModel>();
        //    using (MyDBEntities db = new MyDBEntities())
        //    {
        //        List<HolidayDetail> holidayDetails = db.HolidayDetails.Where(w => w.EId == id).ToList();
        //        foreach (HolidayDetail detail in holidayDetails)
        //        {
        //            holidayDetailViewModels.Add(new HolidayDetailViewModel()
        //            {
        //                Id = detail.Id,
        //                EId = detail.EId,
        //                Name = detail.Employee.Name,
        //                eEnName = detail.Employee.englishName,
        //                beginDate = detail.beginDate,
        //                endDate = detail.endDate,
        //                state = detail.state,
        //                holidayTitle = detail.Holiday.title,
        //                usedDays = detail.usedDays,
        //                usedHours = detail.usedHours,
        //                remark = detail.remark
        //            });
        //        }
        //    }
        //    return holidayDetailViewModels;
        //}
    }
}