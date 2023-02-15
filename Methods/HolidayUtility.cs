using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UCOMProject.Extension;
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
                //統計今年度已用及未用假別天數
                List<Holiday> holidays = db.Holidays.OrderBy(o => o.Id).ToList();
                if (emp.Sex == "男")
                {
                    holidays.Remove(holidays.Find(f => f.Title.xTranEnum() == HolidayType.生理假));
                }
                foreach (Holiday item in holidays)
                {
                    HolidayModel holidayModel = new HolidayModel(emp.StartDate)
                    {
                        Id = item.Id,
                        TitleType = item.Title.xTranEnum(),
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
        public static Dictionary<int, List<HolidayViewModel.Chart>> getchartDictByEmpID(string id)
        {
            Dictionary<int, List<HolidayViewModel.Chart>> chartsDict = new Dictionary<int, List<HolidayViewModel.Chart>>();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.SingleOrDefault(s => s.EId == id);
                //查詢指定年度的每月休假天數總計
                List<HolidayViewModel.Chart> chartGroup = emp.HolidayDetails.GroupBy(g => (g.BeginDate.Year, g.BeginDate.Month), g => g.UsedDays)
                .Select(s => new HolidayViewModel.Chart
                {
                    Year = s.Key.Year,
                    Month = s.Key.Month,
                    Days = s.Sum()
                }).OrderBy(o => o.Year).ThenBy(t => t.Month).ToList();

                //整合charts所需要的資訊
                foreach (int year in chartGroup.Select(s => s.Year).Distinct())
                {
                    List<HolidayViewModel.Chart> chartList = new List<HolidayViewModel.Chart>();
                    for (int month = 1; month <= 12; month++)
                    {
                        HolidayViewModel.Chart chartInfo = chartGroup.Find(x => x.Year == year && x.Month == month);
                        if (chartInfo == null)
                        {
                            chartList.Add(new HolidayViewModel.Chart() { Year = year, Month = month, Days = 0 });
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
        /// 休假申請
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>是否成功?異常訊息?</returns>
        public static bool checkApply(HolidayDetailModel payload, out string applyMsg)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = db.HolidayDetails
                    .Where(w => w.EId == payload.EId)
                    .OrderBy(o => o.BeginDate)
                    .Select(s => new { BeginDate = s.BeginDate, EndDate = s.EndDate });

                //取得payloadMonths裡的休假月份 , 用來快速索引query的休假月份是否相符
                List<int> payloadMonths = payload.RangeDate.Select(s => ((DateTime)s).Month).OrderBy(o => o).Distinct().ToList();

                foreach (var item in query)
                {
                    //判斷該員工休假detail之中有沒有與申請休假的月份相同 ,Any查到相符返回true , 進一步查詢是否有相符的日期
                    if (payloadMonths.Any(month => month == item.BeginDate.Month || month == item.EndDate.Month))
                    {
                        //checkDays計算detail結束日期-開始日期的總天數
                        int checkDays = item.EndDate.Subtract(item.BeginDate).Days;
                        while (checkDays >= 0)
                        {
                            //請假範圍 >= 0天(包括當天) 就從結束的日期 - 要判斷的總天數開始判斷
                            DateTime checkDate = item.EndDate.AddDays(checkDays * -1);

                            //payload送過來的RangeDate含有員工選擇休假期間的所有日期集合,拿來判斷是否跟資料庫的日期是否重複
                            var result = payload.RangeDate
                                   .Any(date =>
                                   (new DateTime(date.Year, date.Month, date.Day)).Ticks
                                   .Equals(new DateTime(checkDate.Year, checkDate.Month, checkDate.Day).Ticks));
                            //如果申請的日期已有休假紀錄 , 就回傳false並儲存錯誤資訊
                            if (result)
                            {
                                if (checkDays == 0)
                                {
                                    //休假紀錄只有一天,就返回該日期
                                    applyMsg = $"{item.BeginDate.ToShortDateString()}\r\n已有休假紀錄，請重新申請";
                                }
                                else
                                {
                                    //休假紀錄超過一天,就返回開始日期~結束日期
                                    applyMsg = $"{item.BeginDate.ToShortDateString()} ~ {item.EndDate.ToShortDateString()}\r\n已有休假紀錄，請重新申請";
                                }
                                return false;
                            }
                            checkDays--;
                        }
                    }
                }

                //判斷結束無休假紀錄，準備將資料新增到資料庫
                HolidayDetail holidayDetail = new HolidayDetail()
                {
                    EId = payload.EId,
                    HId = (int)(payload.HId),
                    BeginDate = (DateTime)payload.BeginDate,
                    EndDate = (DateTime)payload.EndDate,
                    UsedDays = (int)payload.UsedDays,
                    Allow = false,
                    Remark = payload.Remark,
                };
                db.HolidayDetails.Add(holidayDetail);
                db.SaveChangesAsync();

                applyMsg = $"申請已送出！\r\n" +
                           $"{((DateTime)payload.BeginDate).ToShortDateString()} ~ " +
                           $"{((DateTime)payload.EndDate).ToShortDateString()}\r\n共計：{payload.Title}{payload.UsedDays}天";
                return true;
            }
        }

        /// <summary>
        /// 查詢休假歷史紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static List<HolidayDetailViewModel> getHolidayDetailsByEmpID(string id)
        //{
        //    List<HolidayDetailViewModel>    = new List<HolidayDetailViewModel>();
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