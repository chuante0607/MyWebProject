using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    public class apiScheduleController : ApiController
    {
        // GET: api/apiSchedule
        public async Task<IHttpActionResult> Get()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var result = await HolidayUtility.GetHolidayDetails("E006");
                List<CalendarViewModel> calendars = new List<CalendarViewModel>();
                foreach(var item in result)
                {
                    calendars.Add(new CalendarViewModel
                    {
                        id = item.Id.ToString(),
                        title = item.Title,
                        start = item.BeginDate,
                        end = item.EndDate,
                        color = Color.Red.ToArgb()
                    }); ;
                }
                return Json(calendars);
            }
        }

        public class CalendarViewModel
        {
            public string id { get; set; }
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public int color { get; set; }
        }

        // GET: api/apiSchedule/5
        public string Get(int id)
        {
            return "value";
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
