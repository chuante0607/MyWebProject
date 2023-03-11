using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<IHttpActionResult> Get()
        {
            var req = Request.Headers.Where(s => s.Key == "ROLE_CODE")
                .Select(s => s.Value)
                .ToList();

            ScheduleViewModel schedule = new ScheduleViewModel();
            schedule.Shifts = ScheduleUtility.GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
            schedule.Calendars = await GetCalendars();
            schedule.LeaveNums = DateCount;
            return Json(JsonConvert.SerializeObject(schedule, camelSetting));
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            return Ok();
        }

        private async Task<List<CalendarViewModel>> GetCalendars()
        {
            List<HolidayDetailViewModel> holidayDetails = await HolidayUtility.GetHolidayDetails();
            List<CalendarViewModel> calendars = new List<CalendarViewModel>();
            foreach (HolidayDetailViewModel detail in holidayDetails)
            {
                string className = "";
                if (detail.State != 2)
                    continue;
                switch (detail.Shift.xTranShiftEnum())
                {
                    case ShiftType.常日班:
                        break;
                    case ShiftType.A班:
                        className = "event_shiftA";
                        break;
                    case ShiftType.B班:
                        className = "event_shiftB";
                        break;
                    default:
                        break;
                }
                foreach (DateTime date in detail.RangDate)
                {
                    CalendarViewModel calendar = new CalendarViewModel();
                    //統計請假人數
                    if (DateCount.ContainsKey(date))
                    {
                        DateCount[date]++;
                    }
                    else
                    {
                        DateCount.Add(date, 1);
                    }
                    calendar.Id = detail.Id.ToString();
                    calendar.Title = $"{detail.Name}-{detail.Title}({EvenDaysList.Count + 1}天)";
                    calendar.Start = date;
                    calendar.End = date.AddDays(1);
                    calendar.ClassNames = className;
                    calendars.Add(calendar);
                    ////每個新的日期就先判斷EvenDaysList有無符合?符合表示為連續日期
                    //if (EvenDaysList.Count > 0 && EvenDaysList.Any(a => a == date))
                    //    continue;
                    ////沒有符合表示為新的一個日期
                    //EvenDaysList = new List<DateTime>();
                    ////在使用遞迴檢查是否為連續請假日
                    //DateTime endDate = EvenDays(item.RangDate, date);
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
