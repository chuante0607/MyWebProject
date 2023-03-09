using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiScheduleController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        List<DateTime> EvenDaysList = new List<DateTime>();
        Dictionary<DateTime, int> DateCount = new Dictionary<DateTime, int>();
        [HttpGet]
        /// <summary>
        ///設定calendar A B班顏色
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var req = Request.Headers.Where(s => s.Key == "ROLE_CODE")
                .Select(s => s.Value)
                .ToList();

            ScheduleViewModel schedule = new ScheduleViewModel();
            schedule.Shifts = HolidayUtility.GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
            schedule.Calendars = await GetCalendars();
            schedule.LeaveNums = DateCount;
            return Json(JsonConvert.SerializeObject(schedule, camelSetting));
        }

        [HttpGet]
        public IHttpActionResult Get(string branch)
        {
            var req = Request;

            var emp = SessionEmp.CurrentEmp;
            return Ok();
        }

        public class CalendarViewModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Color { get; set; }
            public string TextColor { get; set; }
            public bool AllDay { get; set; }
        }
        public class CalendarleavePeopleViewModel
        {
            public DateTime StartLeave { get; set; }
            public int PeopleNum { get; set; }
        }

        public class ScheduleViewModel
        {
            public List<CalendarViewModel> Calendars { get; set; }
            public List<List<ShiftViewModel>> Shifts { get; set; }
            public List<CalendarleavePeopleViewModel> PeopleNums { get; set; }
            public Dictionary<DateTime, int> LeaveNums { get; set; }
        }

        private async Task<List<CalendarViewModel>> GetCalendars()
        {
            List<HolidayDetailViewModel> holidayDetails = await HolidayUtility.GetHolidayDetails();
            List<CalendarViewModel> calendars = new List<CalendarViewModel>();
            foreach (HolidayDetailViewModel item in holidayDetails)
            {
                if (item.State != 2)
                    continue;
                string color = "";
                string txtColor = "";
                switch (item.Shift.xTranShiftEnum())
                {
                    case ShiftType.常日班:
                        color = "#F5B6E5";
                        txtColor = "black";
                        break;
                    case ShiftType.A班:
                        color = "#17a2b8";
                        txtColor = "white";
                        break;
                    case ShiftType.B班:
                        color = "#6c757d";
                        txtColor = "white";
                        break;
                    default:
                        break;
                }
                foreach (DateTime date in item.RangDate)
                {
                    //統計請假人數
                    if (DateCount.ContainsKey(date))
                    {
                        DateCount[date]++;
                    }
                    else
                    {
                        DateCount.Add(date, 1);
                    }

                    ////每個新的日期就先判斷EvenDaysList有無符合?符合表示為連續日期
                    //if (EvenDaysList.Count > 0 && EvenDaysList.Any(a => a == date))
                    //    continue;
                    ////沒有符合表示為新的一個日期
                    //EvenDaysList = new List<DateTime>();
                    ////在使用遞迴檢查是否為連續請假日
                    //DateTime endDate = EvenDays(item.RangDate, date);
                    calendars.Add(new CalendarViewModel
                    {
                        Id = item.Id.ToString(),
                        Title = $"{item.Shift}-{item.Name}-{item.Title}({EvenDaysList.Count + 1}天)",
                        Start = date,
                        End = date.AddDays(1),
                        Color = color,
                        TextColor = txtColor,
                        AllDay = true,
                    });
                }
            }
            return calendars;
        }
        /// <summary>
        /// 連續請假天數
        /// </summary>
        /// <param name="days"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private DateTime EvenDays(List<DateTime> days, DateTime currentDate)
        {
            DateTime endDate = currentDate.AddDays(1);
            bool even = days.Any(a => a == endDate);
            if (even)
            {
                //連續的日期存入EvenDaysList
                EvenDaysList.Add(endDate);
                //重複呼叫自己
                endDate = EvenDays(days, endDate);
            }
            return endDate;
        }

        private RoleManage ConfirmIdentity(int rank, BranchType branch)
        {
            RoleManage user = null;
            switch (rank)
            {
                case 0:
                    user = new Admin(RoleType.Admin);
                    break;
                case 1:
                    user = new User(RoleType.User);
                    break;
                case 2:
                    user = new Manager(RoleType.Manager, branch);
                    break;
            }
            return user;
        }
        // POST: api/apiSchedule
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/apiSchedule/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/apiSchedule/5
        public void Delete(int id)
        {
        }
    }
}
