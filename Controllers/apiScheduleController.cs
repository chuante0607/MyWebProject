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
using UCOMProject.API;
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiScheduleController : ApiController
    {
        List<DateTime> EvenDaysList = new List<DateTime>();

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var req = Request.Headers.Where(s => s.Key == "ROLE_CODE")
                .Select(s => s.Value)
                .ToList();

            ScheduleApiModel schedule = new ScheduleApiModel();
            schedule.calendars = await ScheduleUtility.GetCalendars();
            schedule.employees = await EmployeeUtility.GetEmployees();
            schedule.plans = await ScheduleUtility.GetPlansByDay();
            schedule.shifts = ScheduleUtility.GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
            schedule.leaveCount = ScheduleUtility.LeaveCount;
            return Json(schedule);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            return Ok();
        }

        /// <summary>
        /// 連續請假天數
        /// </summary>
        /// <param name="days"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        //private DateTime EvenDays(List<DateTime> days, DateTime currentDate)
        //{
        //    DateTime endDate = currentDate.AddDays(1);
        //    bool even = days.Any(a => a == endDate);
        //    if (even)
        //    {
        //        //連續的日期存入EvenDaysList
        //        EvenDaysList.Add(endDate);
        //        //重複呼叫自己
        //        endDate = EvenDays(days, endDate);
        //    }
        //    return endDate;
        //}

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
