using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UCOMProject.Extension;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Methods
{
    public static class HolidayUtility
    {
        public class NationalHoliday
        {
            public NationalHoliday(DateTime date, bool isWork, string remark)
            {
                Date = date;
                IsWorkDay = isWork;
                Remark = remark;
            }
            public DateTime Date { get; set; }
            public bool IsWorkDay { get; set; }
            public string Remark { get; set; }
        }

        const int WorkCycle = 4;

        /// <summary>
        /// 員工今年度休假天數總覽
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayViewModel>> GetHolidayInfosByEmp(string eid)
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
        public static async Task<List<ChartViewModel>> GetchartInfos(string eid, int year)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                //依照休假年份群組化
                var query = await db.HolidayDetails.Where(s => s.EId == eid && s.State == 2).GroupBy(g => g.BelongYear, g => new { g.BeginDate, g.UsedDays }).ToListAsync();
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
        public static List<List<ShiftViewModel>> GetWorkDayOfYearByMonth(ShiftType shift, int year)
        {
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
                    if (str[2] != "2")
                    {

                        ShiftViewModels.Add(new ShiftViewModel(date, true));
                    }
                    else
                    {
                        ShiftViewModels.Add(new ShiftViewModel(date, false));
                    }
                }
            }
            workDayByMonth = ShiftViewModels.GroupBy(g => g.CheckDate.Month, g => new ShiftViewModel(g.CheckDate, g.IsWork)).Select(g => g.ToList()).ToList();
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
                                return new ApplyResult { isPass = false, msg = applyMsg };
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
                    ApplyDate = DateTime.Now,
                    BeginDate = payload.BeginDate,
                    EndDate = payload.EndDate,
                    BelongYear = payload.BeginDate.Year,
                    UsedDays = payload.UsedDays,
                    Remark = payload.Remark,
                    Prove = fileStr,
                    State = (int)ReviewType.Wait,
                };
                db.HolidayDetails.Add(holidayDetail);
                db.SaveChanges();

                applyMsg = $"申請已送出！\r\n" +
                           $"{((DateTime)payload.BeginDate).ToShortDateString()} ~ " +
                           $"{((DateTime)payload.EndDate).ToShortDateString()}\r\n共計：{payload.Title}{payload.UsedDays}天";
                return new ApplyResult { isPass = true, msg = applyMsg, FilesNames = fileNames };
            }
        }

        /// <summary>
        /// 依員工資訊查詢全部休假紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayDetailViewModel>> GetHolidayDetails(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                List<HolidayDetailViewModel> vmList = new List<HolidayDetailViewModel>();
                var details = await db.HolidayDetails.Where(w => w.EId == eid).OrderByDescending(o => o.Id).ToListAsync();
                var emp = await db.Employees.FindAsync(eid);
                foreach (var detail in details)
                {
                    vmList.Add(await SetDetailViewModelData(detail, emp));
                }
                return vmList;
            }
        }

        /// <summary>
        /// 依部門查詢員工全部休假紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayDetailViewModel>> GetHolidayDetails(BranchType type)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                List<HolidayDetailViewModel> vmList = new List<HolidayDetailViewModel>();
                var emps = await db.Employees.Where(e => e.Branch == type.ToString()).ToListAsync();
                foreach (var emp in emps)
                {
                    foreach (var detail in emp.HolidayDetails)
                    {
                        vmList.Add(await SetDetailViewModelData(detail, emp));
                    }
                }
                return vmList;
            }
        }

        /// <summary>
        /// 查詢全部員工休假紀錄
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                List<HolidayDetailViewModel> vmList = new List<HolidayDetailViewModel>();
                var emps = await db.Employees.ToListAsync();
                foreach (var emp in emps)
                {
                    foreach (var detail in emp.HolidayDetails)
                    {
                        vmList.Add(await SetDetailViewModelData(detail, emp));
                    }
                }
                return vmList;
            }
        }

        /// <summary>
        /// 設定ViewModel資料
        /// </summary>
        /// <param name="data"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        private static async Task<HolidayDetailViewModel> SetDetailViewModelData(HolidayDetail data, Employee emp)
        {
            HolidayDetailViewModel vm = new HolidayDetailViewModel();
            vm.EId = data.EId;
            vm.Name = emp.Name;
            vm.Shift = emp.Shift;
            vm.Branch = emp.Branch;
            vm.Head = await EmployeeUtility.GetEmpById(emp.Branch1.Head);
            vm.Id = data.Id;
            vm.HId = data.HId;
            vm.Title = data.Holiday.Title;
            vm.UsedDays = data.UsedDays;
            vm.BeginDate = data.BeginDate;
            vm.EndDate = data.EndDate;
            vm.State = data.State;
            vm.Remark = data.Remark;
            vm.Prove = data.Prove;
            vm.ApplyDate = data.ApplyDate;
            vm.Reason = data.Reason;
            if (data.AllowManager != null)
                vm.AllowManager = await EmployeeUtility.GetEmpById(data.AllowManager);
            return vm;
        }

        /// <summary>
        /// 修改休假申請狀態
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<bool> EditHolidayDetailsState(List<HolidayDetailViewModel> data, ReviewType state, Employee allowManager)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                List<int> ids = data.Select(s => s.Id).ToList();
                var details = await db.HolidayDetails.Where(w => ids.Contains(w.Id)).ToListAsync();
                foreach (var detail in details)
                {
                    switch (state)
                    {
                        case ReviewType.Wait:
                            detail.Reason = null;
                            break;
                        case ReviewType.Pass:
                            detail.Reason = null;
                            break;
                        case ReviewType.Back:
                            detail.Reason = data.Find(f => f.Id == detail.Id).Reason;
                            break;
                        default:
                            break;
                    }
                    detail.State = (int)state;
                    detail.AllowManager = allowManager.EId;
                }
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 刪除待審核的休假申請
        /// </summary>
        /// <param name="id"></param>
        public static async Task<bool> DelHolidayDetails(List<int> id, string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var result = await db.HolidayDetails.Where(w => id.Contains(w.Id) && w.EId == eid).ToListAsync();
                if (result == null)
                {
                    return false;
                }
                db.HolidayDetails.RemoveRange(result);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}