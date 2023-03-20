using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.API;
using UCOMProject.Extension;
using UCOMProject.Models;
using UCOMProject.Roles;

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
        /// 取得每天的plans人力計畫
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Plan>> GetPlansByDay()
        {
            List<Plan> list = new List<Plan>();
            List<Plan> plans = await GetPlans();
            //(統計"每天"需求人力)
            foreach (Plan p in plans)
            {
                //plan只有start跟end 所以要轉成每天的人力所有-1天表示當天
                p.EndDate = p.EndDate.AddDays(-1);
                TimeSpan span = p.EndDate.Subtract(p.StartDate);
                int day = span.Days;
                for (int i = 0; i <= day; i++)
                {
                    list.Add(new Plan { Id = p.Id, PlanTitle = p.PlanTitle, StartDate = p.StartDate.AddDays(i), EndDate = p.StartDate.AddDays(i) });
                }
            }
            return list.OrderBy(o => o.StartDate).ToList();
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
        public static async Task<List<List<ShiftViewModel>>> GetWorkDayOfYearByMonth(ShiftType shift, int year)
        {
            //using (MyDBEntities db = new MyDBEntities())
            //{
            //    List<List<ShiftViewModel>> workDayByMonth = new List<List<ShiftViewModel>>();
            //    var query = await db.Attendances.ToListAsync();
            //    Dictionary<int, List<Attendance>> group = query.GroupBy(g => g.WorkDate.Month).ToDictionary(t => t.Key, t => t.ToList());
            //    int month = 1;
            //    while (true)
            //    {
            //        if (group.ContainsKey(month))
            //        {
            //            List<ShiftViewModel> shifts = new List<ShiftViewModel>();
            //            foreach (Attendance attendance in group[month])
            //            {
            //                if(shift.ToString() == attendance.Shift)
            //                {
            //                    shifts.Add(new ShiftViewModel(shift, attendance.WorkDate, true));
            //                }
            //                else
            //                {
            //                    shifts.Add(new ShiftViewModel(shift, attendance.WorkDate, false));
            //                }
            //            }
            //            workDayByMonth.Add(shifts);
            //        }
            //        else
            //        {
            //            break;
            //        }
            //        month++;
            //    }
            //    return workDayByMonth;
            //}

            List<List<ShiftViewModel>> workDayByMonth = new List<List<ShiftViewModel>>();
            const int WorkCycle = 4;
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

        public static async Task<ShiftType> GetShiftTypeByDate(DateTime date, int year)
        {
            var query = await GetWorkDayOfYearByMonth(ShiftType.A班, year);
            ShiftViewModel result = query[date.Month - 1].Find(s => s.CheckDate == date);
            if (result != null)
            {
                //A班工作就取B班 反之亦然
                return result.IsWork ? ShiftType.B班 : ShiftType.A班;
            }
            else
            {
                return ShiftType.請選擇;
            }
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
            using (MyDBEntities db = new MyDBEntities())
            {
                foreach (var line in file)
                {
                    List<string> str = line.Split(',').ToList();
                    if (DateTime.TryParseExact(str[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    {
                        if (str[2] == "2")
                        {
                            ShiftViewModels.Add(new ShiftViewModel(ShiftType.常日班, date, false));
                        }
                        else
                        {
                            ShiftViewModels.Add(new ShiftViewModel(ShiftType.常日班, date, true));
                        }
                    }
                }
            }

            workDayByMonth = ShiftViewModels.GroupBy(g => g.CheckDate.Month).Select(s => s.ToList()).ToList();
            return workDayByMonth;
        }

        /// <summary>
        /// 取得請假人數在Calendars所展示的資訊
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CalendarApiModel>> GetCalendars(RoleManage user)
        {
            var query = await user.GetHolidayDetails();
            List<CalendarApiModel> calendars = new List<CalendarApiModel>();
            List<HolidayDetailViewModel> holidayDetails = query.ToList();
            foreach (HolidayDetailViewModel detail in holidayDetails)
            {
                string className = "";
                string backColor = "";
                string txtColor = "";
                if (detail.State != 2)
                    continue;
                switch (detail.Shift.xTranShiftEnum())
                {
                    case ShiftType.常日班:
                        className = "event_shiftW";
                        backColor = "#FED3D3";
                        txtColor = "#000";
                        break;
                    case ShiftType.A班:
                        className = "event_shiftA";

                        break;
                    case ShiftType.B班:
                        className = "event_shiftB";
                        backColor = "#fff";
                        txtColor = "#000";

                        break;
                    default:
                        break;
                }
                foreach (DateTime date in detail.RangDate)
                {
                    CalendarApiModel calendar = new CalendarApiModel();
                    calendar.id = detail.Id.ToString();
                    calendar.title = $"{detail.Name}";
                    calendar.start = date;
                    calendar.end = date.AddDays(1);
                    calendar.classNames = className;
                    calendar.backgroundColor = backColor;
                    calendar.textColor = txtColor;
                    calendar.remark = $"{ detail.Title} {detail.UsedDays}天：{detail.BeginDate.ToString("M/d")} - {detail.EndDate.ToString("M/d")}";
                    calendar.shift = detail.Shift;
                    calendar.eid = detail.EId;
                    calendars.Add(calendar);
                }
            }
            calendars = calendars.OrderBy(o => o.classNames).ToList();
            return calendars;
        }

        /// <summary>
        /// Schedule頁面的所有資訊
        /// </summary>
        /// <returns></returns>
        public static async Task<ScheduleApiModel> GetSchedule(RoleManage user)
        {
            ScheduleApiModel schedule = new ScheduleApiModel();
            schedule.calendars = await GetCalendars(user);
            if (user.GetRole() == RoleType.User)
            {
                EmployeeViewModel result = await user.GetUser();
                List<EmployeeViewModel> emplist = new List<EmployeeViewModel>();
                emplist.Add(result);
                schedule.employees = emplist;
            }
            else
            {
                schedule.employees = await user.GetEmployees();
            }
            var details = await user.GetHolidayDetails();
            schedule.plans = await GetPlans();
            string[] file = System.IO.File.ReadAllLines(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/112年中華民國政府行政機關辦公日曆表.csv"), Encoding.Default);
            schedule.shifts = await GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
            schedule.weekWorks = GetWorkDayOfYearByMonth(file, DateTime.Now.Year);
            int empsA = schedule.employees.Where(e => e.ShiftType == ShiftType.A班).ToList().Count();
            int empsB = schedule.employees.Where(e => e.ShiftType == ShiftType.B班).ToList().Count();
            int empsW = schedule.employees.Where(e => e.ShiftType == ShiftType.常日班).ToList().Count();

            //計算每天請假的人數
            schedule.leaveInfos = schedule.calendars.GroupBy(g => g.start).Select(group => new LeaveNumApiModel { date = group.Key, leaveNum = group.Count() }).ToList();

            //以下計算人力
            List<ScheduleNumApiModel> list = new List<ScheduleNumApiModel>();
            foreach (Plan p in schedule.plans.OrderBy(p => p.StartDate))
            {
                //plan日期是範圍
                TimeSpan days = p.EndDate.Subtract(p.StartDate);
                for (int i = 0; i < days.Days; i++)
                {
                    DateTime planDate = p.StartDate.AddDays(i);
                    ScheduleNumApiModel numModel = new ScheduleNumApiModel();
                    //shifts weekDays leaves索引由0開始表示1月
                    int month = planDate.Month - 1;
                    //planNum當天排程需求人力
                    numModel.planNum = int.Parse(p.PlanTitle);
                    //確認當天是A或B班出勤
                    ShiftViewModel shiftByDate = schedule.shifts[month].FirstOrDefault(shift => shift.CheckDate == planDate);
                    //確認當天常日班是否出勤
                    ShiftViewModel weekDayByDate = schedule.weekWorks[month].FirstOrDefault(shift => shift.CheckDate == planDate);
                    if (shiftByDate != null && weekDayByDate != null)
                    {
                        //shouldNum 當天應出勤出勤人力
                        numModel.shouldNum += shiftByDate.IsWork ? empsA : empsB;
                        numModel.shouldNum += weekDayByDate.IsWork ? empsW : 0;
                        //當天請假人力
                        numModel.leaveNum = details.Where(d => d.State == 2 && d.RangDate.Contains(planDate)).ToList().Count();
                    }
                    //實際人力不夠  在加到attendance於前端發送通知
                    if (numModel.realNum < 0)
                    {
                        List<string> shifts = new List<string>();
                        numModel.date = planDate;
                        //紀錄出勤班別
                        shifts.Add(shiftByDate.IsWork ? ShiftType.A班.ToString() : ShiftType.B班.ToString());
                        shifts.Add(weekDayByDate.IsWork ? ShiftType.常日班.ToString() : ShiftType.請選擇.ToString());
                        numModel.shouldShifts = shifts;
                        list.Add(numModel);
                    }
                }
            }
            schedule.attendance = list;
            return schedule;
        }

        /// <summary>
        /// 取得自己Calendars所展示的資訊請假與加班
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CalendarApiModel>> GetCalendarsBySelf(string eid)
        {
            var query = await HolidayUtility.GetHolidayDetails(eid);
            List<CalendarApiModel> calendars = new List<CalendarApiModel>();
            List<HolidayDetailViewModel> holidayDetails = query.ToList();
            foreach (HolidayDetailViewModel detail in holidayDetails)
            {
                string className = "";
                string backColor = "";
                string txtColor = "";
                if (detail.State != 2)
                    continue;
                switch (detail.Shift.xTranShiftEnum())
                {
                    case ShiftType.常日班:
                        className = "event_shiftW";
                        backColor = "#FED3D3";
                        txtColor = "#000";
                        break;
                    case ShiftType.A班:
                        className = "event_shiftA";

                        break;
                    case ShiftType.B班:
                        className = "event_shiftB";
                        backColor = "#fff";
                        txtColor = "#000";

                        break;
                    default:
                        break;
                }
                foreach (DateTime date in detail.RangDate)
                {
                    CalendarApiModel calendar = new CalendarApiModel();
                    calendar.id = detail.Id.ToString();
                    calendar.title = $"{detail.Name}";
                    calendar.start = date;
                    calendar.end = date.AddDays(1);
                    calendar.classNames = className;
                    calendar.backgroundColor = backColor;
                    calendar.textColor = txtColor;
                    calendar.remark = $"{ detail.Title} {detail.UsedDays}天：{detail.BeginDate.ToString("M/d")} - {detail.EndDate.ToString("M/d")}";
                    calendar.shift = detail.Shift;
                    calendar.eid = detail.EId;
                    calendars.Add(calendar);
                }
            }
            calendars = calendars.OrderBy(o => o.classNames).ToList();
            return calendars;
        }

        /// <summary>
        /// Schedule頁面的所有資訊
        /// </summary>
        /// <returns></returns>
        public static async Task<ScheduleApiModel> GetScheduleBySelf(RoleManage user, string eid)
        {
            ScheduleApiModel schedule = new ScheduleApiModel();
            schedule.calendars = await GetCalendarsBySelf(eid);
            EmployeeViewModel result = await user.GetUser();
            List<EmployeeViewModel> emplist = new List<EmployeeViewModel>();
            emplist.Add(result);
            schedule.employees = emplist;
            var details = await user.GetHolidayDetails();
            schedule.plans = await GetPlans();
            string[] file = System.IO.File.ReadAllLines(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/112年中華民國政府行政機關辦公日曆表.csv"), Encoding.Default);
            schedule.shifts = await GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
            schedule.weekWorks = GetWorkDayOfYearByMonth(file, DateTime.Now.Year);
            int empsA = schedule.employees.Where(e => e.ShiftType == ShiftType.A班).ToList().Count();
            int empsB = schedule.employees.Where(e => e.ShiftType == ShiftType.B班).ToList().Count();
            int empsW = schedule.employees.Where(e => e.ShiftType == ShiftType.常日班).ToList().Count();

            //計算每天請假的人數
            schedule.leaveInfos = schedule.calendars.GroupBy(g => g.start).Select(group => new LeaveNumApiModel { date = group.Key, leaveNum = group.Count() }).ToList();

            //以下計算人力
            List<ScheduleNumApiModel> list = new List<ScheduleNumApiModel>();
            foreach (Plan p in schedule.plans.OrderBy(p => p.StartDate))
            {
                //plan日期是範圍
                TimeSpan days = p.EndDate.Subtract(p.StartDate);
                for (int i = 0; i < days.Days; i++)
                {
                    DateTime planDate = p.StartDate.AddDays(i);
                    ScheduleNumApiModel numModel = new ScheduleNumApiModel();
                    //shifts weekDays leaves索引由0開始表示1月
                    int month = planDate.Month - 1;
                    //planNum當天排程需求人力
                    numModel.planNum = int.Parse(p.PlanTitle);
                    //確認當天是A或B班出勤
                    ShiftViewModel shiftByDate = schedule.shifts[month].FirstOrDefault(shift => shift.CheckDate == planDate);
                    //確認當天常日班是否出勤
                    ShiftViewModel weekDayByDate = schedule.weekWorks[month].FirstOrDefault(shift => shift.CheckDate == planDate);
                    if (shiftByDate != null && weekDayByDate != null)
                    {
                        //shouldNum 當天應出勤出勤人力
                        numModel.shouldNum += shiftByDate.IsWork ? empsA : empsB;
                        numModel.shouldNum += weekDayByDate.IsWork ? empsW : 0;
                        //當天請假人力
                        numModel.leaveNum = details.Where(d => d.State == 2 && d.RangDate.Contains(planDate)).ToList().Count();
                    }
                    //實際人力不夠  在加到attendance於前端發送通知
                    if (numModel.realNum < 0)
                    {
                        List<string> shifts = new List<string>();
                        numModel.date = planDate;
                        //紀錄出勤班別
                        shifts.Add(shiftByDate.IsWork ? ShiftType.A班.ToString() : ShiftType.B班.ToString());
                        shifts.Add(weekDayByDate.IsWork ? ShiftType.常日班.ToString() : ShiftType.請選擇.ToString());
                        numModel.shouldShifts = shifts;
                        list.Add(numModel);
                    }
                }
            }
            schedule.attendance = list;
            return schedule;
        }


        /// <summary>
        /// 取得指定日期所有人員出勤表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<AttendanceViewModel>> GetAttendances(DateTime date)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                DateTime currentDate = date;
                //出勤表
                List<Attendance> attendances = await db.Attendances.ToListAsync();
                Attendance attendance = attendances.FirstOrDefault(f => f.WorkDate == currentDate);
                if (attendance == null)
                {
                    throw new Exception($"{date.ToShortDateString()}沒有資訊");
                }
                ShiftType shift = attendance.Shift.xTranShiftEnum();
                //人員清單
                List<EmployeeViewModel> emps = await EmployeeUtility.GetEmployees();
                if (attendance.WeekWork)
                {
                    //如果常日班上班列入常日班
                    emps = emps.Where(w => w.ShiftType == shift || w.ShiftType == ShiftType.常日班).ToList();
                }
                else
                {
                    //只列入輪班
                    emps = emps.Where(w => w.ShiftType == shift).ToList();
                }
                //請假的人員名單
                List<HolidayDetailViewModel> details = await HolidayUtility.GetHolidayDetails(currentDate);
                details = details.Where(d => d.RangDate.Contains(currentDate)).ToList();


                return setAttendanceViewModel(emps, details, currentDate, true);
            }
        }

        /// <summary>
        /// 取得出勤的班別
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async Task<Attendance> GetShiftAttendances(DateTime date)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Attendance attendance = await db.Attendances.FirstOrDefaultAsync(a => a.WorkDate == date);
                if (attendance == null)
                {
                    throw new Exception($"{date.ToShortDateString()}沒有資訊");
                }
                return attendance;
            }
        }


        /// <summary>
        /// 取得出勤表與可加班人力
        /// </summary>
        /// <param name="emps"></param>
        /// <param name="details"></param>
        /// <param name="attendance"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<AttendanceViewModel> GetAttendances(List<EmployeeViewModel> emps, List<HolidayDetailViewModel> detailsByDate, Attendance attendance, DateTime date)
        {
            List<AttendanceViewModel> attendances = new List<AttendanceViewModel>();
            List<EmployeeViewModel> weekWorkEmps = new List<EmployeeViewModel>();
            List<EmployeeViewModel> weekOverTimeEmps = new List<EmployeeViewModel>();
            List<EmployeeViewModel> shiftWorkEmps = new List<EmployeeViewModel>();
            List<EmployeeViewModel> shiftOverTimeEmps = new List<EmployeeViewModel>();

            //判斷部門裡有無常日班員工
            if (emps.Any(s => s.ShiftType == ShiftType.常日班))
            {
                //紀錄當天應該要出勤人員的名單
                weekWorkEmps = emps.Where(s => s.ShiftType == ShiftType.常日班 && attendance.WeekWork).ToList();
                if (weekWorkEmps.Count == 0)
                {
                    //可以發布加班
                    weekOverTimeEmps = emps.Where(s => s.ShiftType == ShiftType.常日班).ToList();
                }
            }

            //判斷部門裡有無AB班員工
            if (emps.Any(s => s.ShiftType == ShiftType.A班 || s.ShiftType == ShiftType.B班))
            {
                switch (attendance.Shift.xTranShiftEnum())
                {
                    case ShiftType.A班:
                        shiftWorkEmps = emps.Where(w => w.ShiftType == ShiftType.A班).ToList();
                        shiftOverTimeEmps = emps.Where(w => w.ShiftType == ShiftType.B班).ToList();
                        break;
                    case ShiftType.B班:
                        shiftWorkEmps = emps.Where(w => w.ShiftType == ShiftType.B班).ToList();
                        shiftOverTimeEmps = emps.Where(w => w.ShiftType == ShiftType.A班).ToList();
                        break;
                    default:
                        break;
                }

            }
            List<EmployeeViewModel> workEmps = new List<EmployeeViewModel>();
            workEmps.AddRange(weekWorkEmps);
            workEmps.AddRange(shiftWorkEmps);
            List<EmployeeViewModel> overTimeEMps = new List<EmployeeViewModel>();
            overTimeEMps.AddRange(weekOverTimeEmps);
            overTimeEMps.AddRange(shiftOverTimeEmps);

            //請假的人員名單

            List<AttendanceViewModel> workVM = setAttendanceViewModel(workEmps, detailsByDate, date, false);
            List<AttendanceViewModel> overTimeVM = setAttendanceViewModel(overTimeEMps, detailsByDate, date, true);
            List<AttendanceViewModel> vm = new List<AttendanceViewModel>();
            vm.AddRange(workVM);
            vm.AddRange(overTimeVM);
            return vm;
        }

        /// <summary>
        /// 取得部門出勤表指定日期
        /// </summary>
        /// <returns></returns>
        public static async Task<List<AttendanceViewModel>> GetAttendances(DateTime date, BranchType branch)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                DateTime currentDate = date;
                //出勤表
                List<Attendance> attendances = await db.Attendances.ToListAsync();
                Attendance attendance = attendances.FirstOrDefault(f => f.WorkDate == currentDate);
                if (attendance == null)
                {
                    throw new Exception($"{date.ToShortDateString()}沒有資訊");
                }
                ShiftType shift = attendance.Shift.xTranShiftEnum();
                //人員清單
                List<EmployeeViewModel> emps = await EmployeeUtility.GetEmployees(new List<int> { (int)branch });
                if (branch == BranchType.製造部)
                {
                    //主管是製造部就列入輪班
                    emps = emps.Where(w => w.ShiftType == shift).ToList();
                }
                else
                {
                    //否則就只列入常日班
                    emps = emps.Where(w => w.ShiftType == ShiftType.常日班).ToList();
                }

                //請假的人員名單
                List<HolidayDetailViewModel> details = await HolidayUtility.GetHolidayDetails(currentDate);
                details = details.Where(d => d.RangDate.Contains(currentDate)).ToList();

                return setAttendanceViewModel(emps, details, currentDate, true);
            }
        }

        /// <summary>
        /// 計算出勤
        /// </summary>
        /// <param name="emps"></param>
        /// <param name="details"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private static List<AttendanceViewModel> setAttendanceViewModel(List<EmployeeViewModel> emps, List<HolidayDetailViewModel> details, DateTime currentDate, bool canOverTime)
        {
            //統計出勤
            List<AttendanceViewModel> vmList = new List<AttendanceViewModel>();
            foreach (EmployeeViewModel emp in emps)
            {
                AttendanceViewModel vm = new AttendanceViewModel();
                vm.ViewDate = currentDate;
                vm.EId = emp.EId;
                vm.Shift = emp.Shift;
                vm.ShiftType = emp.ShiftType;
                vm.Name = emp.Name;
                vm.EnglishName = emp.EnglishName;
                vm.Image = emp.Image;
                vm.BranchType = emp.BranchType;
                vm.JobType = emp.JobType;
                vm.JobRank = emp.JobRank;
                vm.IsLeave = details.Any(hasEmp => hasEmp.EId == emp.EId);
                vm.CanOverTime = canOverTime;
                //請假的人
                if (vm.IsLeave)
                {
                    HolidayDetailViewModel detail = details.Where(w => w.EId == vm.EId).FirstOrDefault();
                    vm.Id = detail.Id;
                    vm.HId = detail.HId;
                    vm.Title = detail.Title;
                    vm.TitleType = detail.TitleType;
                    vm.BeginDate = detail.BeginDate;
                    vm.EndDate = detail.EndDate;
                    vm.RangDate = detail.RangDate;
                    vm.Remark = detail.Remark;
                }
                vmList.Add(vm);
            }
            return vmList;
        }

    }
}