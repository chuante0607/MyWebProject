using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class ScheduleController : Controller
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        // GET: Production
        public async Task<ActionResult> Index()
        {
            List<Plan> list = new List<Plan>();
            List<Plan> plans = await ScheduleUtility.GetPlans();
            foreach (Plan p in plans)
            {
                p.EndDate = p.EndDate.AddDays(-1);
                TimeSpan span = p.EndDate.Subtract(p.StartDate);
                int day = span.Days;
                for (int i = 0; i <= day; i++)
                {
                    list.Add(new Plan { Id = p.Id, PlanTitle = p.PlanTitle, StartDate = p.StartDate.AddDays(i), EndDate = p.StartDate.AddDays(i) });
                }
            }
            ScheduleViewModel schedule = new ScheduleViewModel();
            schedule.Employees = await EmployeeUtility.GetEmployees();
            schedule.Plans = list.OrderBy(o => o.StartDate).ToList();
            ViewBag.Source = JsonConvert.SerializeObject(schedule, camelSetting);
            return View();
        }

        public async Task<ActionResult> Plan()
        {
            ViewBag.Source = JsonConvert.SerializeObject(await EmployeeUtility.GetEmployees(), camelSetting);
            return View();
        }

    }
}