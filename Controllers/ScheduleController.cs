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
using UCOMProject.API;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class ScheduleController : Controller
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        // GET: Production
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Plan()
        {
            ViewBag.Source = JsonConvert.SerializeObject(await EmployeeUtility.GetEmployees(), camelSetting);
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }
    }
}