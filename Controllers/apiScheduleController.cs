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
            return Json(JsonConvert.SerializeObject(schedule, camelSetting));
        }

        [HttpGet]
        public IHttpActionResult Get(string eid)
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

        public class ScheduleViewModel
        {
            public List<CalendarViewModel> Calendars { get; set; }
            public List<List<ShiftViewModel>> Shifts { get; set; }
        }

        private async Task<List<CalendarViewModel>> GetCalendars()
        {
            List<HolidayDetailViewModel> holidayDetails = await HolidayUtility.GetHolidayDetails();
            List<CalendarViewModel> calendars = new List<CalendarViewModel>();
            foreach (var item in holidayDetails)
            {
                if (item.State != 2)
                    continue;
                string color = "";
                switch (item.Shift.xTranShiftEnum())
                {
                    case ShiftType.常日班:
                        color = "#dc3545";
                        break;
                    case ShiftType.A班:
                        color = "#17a2b8";
                        break;
                    case ShiftType.B班:
                        color = "#6c757d";
                        break;
                    default:
                        break;
                }
                calendars.Add(new CalendarViewModel
                {
                    Id = item.Id.ToString(),
                    Title = $"{item.Shift}-{item.Name}-{item.Title}({item.UsedDays}天)",
                    Start = item.BeginDate,
                    End = item.EndDate.AddDays(1),
                    Color = color,
                    TextColor = "white",
                    AllDay = true,
                });
            }
            return calendars;
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
