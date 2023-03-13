using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.API;
using UCOMProject.Methods;

namespace UCOMProject.Controllers
{
    public class apiEventController : ApiController
    {
        // GET: api/apiEvent
        public async Task<IHttpActionResult> Get()
        {
            List<CalendarApiModel> calendars = new List<CalendarApiModel>();
            calendars = await ScheduleUtility.GetCalendars();
            return Json(calendars);
        }

        // GET: api/apiEvent/5
        public async Task<IHttpActionResult> Get(DateTime start, DateTime end)
        {
            List<CalendarApiModel> calendars = new List<CalendarApiModel>();
            calendars = await ScheduleUtility.GetCalendars();
            return Json(calendars);
        }

        // POST: api/apiEvent
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/apiEvent/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/apiEvent/5
        public void Delete(int id)
        {
        }
    }
}
