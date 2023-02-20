using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UCOMProject.Extension;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public static class HolidayUtility
    {
        /// <summary>
        /// 員工今年度休假天數總覽
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayViewModel>> GetHolidayInfos(string eid)
        {
            List<HolidayViewModel> holidayViewModels = new List<HolidayViewModel>();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.Find(eid);
                List<Holiday> holidays = null;

                if (emp.Sex == "男")
                    holidays = await db.Holidays.Where(w => w.AnyOne).OrderBy(o => o.HId).ToListAsync();
                else
                    holidays = await db.Holidays.OrderBy(o => o.HId).ToListAsync();
                foreach (Holiday item in holidays)
                {
                    HolidayViewModel holiday = new HolidayViewModel(emp.StartDate)
                    {
                        HId = item.HId,
                        Title = item.Title,
                        ProveType = item.ProveType,
                        TotalDays = item.TotalDays,
                        UsedDays = emp.HolidayDetails
                        .Where(w => w.HId == item.HId && w.BelongYear == DateTime.Now.Year)
                        .Select(s => s.UsedDays).Sum(),
                    };
                    holidayViewModels.Add(holiday);
                }
                return holidayViewModels;
            }
        }

        /// <summary>
        /// 查詢charts資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<ChartViewModel>> GetchartInfos(string eid , int year)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                //依照休假年份群組化
                var query = await db.HolidayDetails.Where(s => s.EId == eid).GroupBy(g => g.BelongYear, g => new { g.BeginDate, g.UsedDays }).ToListAsync();
                List<ChartViewModel> vmList = new List<ChartViewModel>();
                foreach (var item in query)
                {
                    ChartViewModel vm = new ChartViewModel();
                    vm.Year = item.Key;
                    vm.Days = new List<int>();
                    vm.Active = year == item.Key;
                    //1月索引0開始 
                    for (int month = 1; month <= 12; month++)
                    {
                        //遍歷每個月找開始請假的月份是否有符合
                        bool hasUsedDays = item.Any(w => w.BeginDate.Month == month);
                        if (hasUsedDays)
                        {
                            //有休假則統計天數
                            vm.Days.Add(item.Where(w => w.BeginDate.Month == month).Select(s => s.UsedDays).Sum());
                        }
                        else
                        {
                            //無休假則0天
                            vm.Days.Add(0);
                        }
                    }
                    vmList.Add(vm);
                }
                if (query.Count() < 1)
                {
                    ChartViewModel chartViewModel = new ChartViewModel();
                    chartViewModel.Year = DateTime.Now.Year;
                    chartViewModel.Days = new List<int>();
                    chartViewModel.Active = true;
                    //1月索引0開始 
                    for (int month = 1; month <= 12; month++)
                        chartViewModel.Days.Add(0);
                    vmList.Add(chartViewModel);
                }
                return vmList.OrderByDescending(o => o.Year).ToList();
            }
        }

        /// <summary>
        /// 取得A.B班的當年度的工作天(做2休2)
        /// </summary>
        public static List<List<ShiftWork>> GetWorkDayOfYearByMonth(int year)
        {
            //to do:計算做2休2的週期
            const int workCycle = 4;
            List<List<ShiftWork>> workDayByMonth = new List<List<ShiftWork>>();
            for (int month = 1; month <= 12; month++)
            {
                List<ShiftWork> workDays = new List<ShiftWork>();
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
                    workDays.Add(new ShiftWork(type, currentDate));
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
        /// <returns></returns>
        public static ApplyResult SaveApply(ApplyViewModel payload)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                string applyMsg = string.Empty;
                var query = db.HolidayDetails
                    .Where(w => w.EId == payload.EId)
                    .OrderBy(o => o.BeginDate)
                    .Select(s => new { BeginDate = s.BeginDate, EndDate = s.EndDate });

                //取得payloadMonths裡的休假月份 , 用來快速索引query的休假月份是否相符
                List<int> payloadMonths = payload.RangeDate.Select(s => s.Month).OrderBy(o => o).Distinct().ToList();

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

                            if (result)  //如果申請的日期已有休假紀錄 
                            {
                                //休假紀錄只有一天,就返回該日期 , 超過一天,就返回開始日期~結束日期
                                applyMsg = item.EndDate.Subtract(item.BeginDate).Days == 0 ?
                                   $"{item.BeginDate.ToShortDateString()}\r\n已有休假紀錄，請重新申請" :
                                   $"{item.BeginDate.ToShortDateString()} ~ {item.EndDate.ToShortDateString()}\r\n已有休假紀錄，請重新申請";
                                return new ApplyResult { Error = true, Msg = applyMsg };
                            }
                            checkDays--;
                        }
                    }
                }
                //設定檔案檔名
                List<string> fileNames = new List<string>();
                string fileStr = null;
                if (payload.Files != null)
                {
                    var file = payload.Files.Select(s => s.FileName);
                    foreach (var item in file.Select((value, index) => new { value, index }))
                    {
                        string format = $"{DateTime.Now.Ticks}{item.value}";
                        fileNames.Add(format);
                        fileStr += format;
                        if (item.index < file.Count() - 1)
                            fileStr += ",";
                    }
                }
                //判斷結束無休假紀錄，準備將資料新增到資料庫
                HolidayDetail holidayDetail = new HolidayDetail()
                {
                    EId = payload.EId,
                    HId = payload.HId,
                    BeginDate = payload.BeginDate,
                    EndDate = payload.EndDate,
                    BelongYear = payload.BeginDate.Year,
                    UsedDays = payload.UsedDays,
                    Allow = false,
                    Remark = payload.Remark,
                    Prove = fileStr,
                };
                db.HolidayDetails.Add(holidayDetail);
                db.SaveChanges();

                applyMsg = $"申請已送出！\r\n" +
                           $"{((DateTime)payload.BeginDate).ToShortDateString()} ~ " +
                           $"{((DateTime)payload.EndDate).ToShortDateString()}\r\n共計：{payload.Title}{payload.UsedDays}天";
                return new ApplyResult { Error = false, Msg = applyMsg, FilesName = fileNames };
            }
        }

        /// <summary>
        /// 查詢休假歷史紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayDetailViewModel>> GetHolidayDetailList(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                List<HolidayDetailViewModel> vmList = new List<HolidayDetailViewModel>();
                var query = await db.HolidayDetails.Where(w => w.EId == eid).OrderByDescending(o => o.BeginDate).ToListAsync();
                var emp = await db.Employees.FindAsync(eid);
                foreach (var item in query)
                {
                    HolidayDetailViewModel vm = new HolidayDetailViewModel();
                    vm.EId = emp.EId;
                    vm.Name = emp.Name;
                    vm.Sex = emp.Sex;
                    vm.Shift = emp.Shift;
                    vm.Id = item.Id;
                    vm.HId = item.HId;
                    vm.Title = item.Holiday.Title;
                    vm.UsedDays = item.UsedDays;
                    vm.BeginDate = item.BeginDate;
                    vm.EndDate = item.EndDate;
                    vm.Allow = item.Allow;
                    vm.Remark = item.Remark;
                    vm.Prove = item.Prove;
                    vmList.Add(vm);
                }
                return vmList;
            }
        }

        /// <summary>
        /// 刪除休假歷史紀錄
        /// </summary>
        /// <param name="id"></param>
        public static void DelHolidayDetail(int id, string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var result = db.HolidayDetails.FirstOrDefault(f => f.Id == id && f.EId == eid);
                if (result != null)
                {
                    db.HolidayDetails.Remove(result);
                    db.SaveChanges();
                }
            }
        }
    }
}