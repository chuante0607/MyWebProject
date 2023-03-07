using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class ScheduleController : Controller
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        // GET: Production
        public async Task<ActionResult> Index()
        {
            var query = await ScheduleUtility.GetSchedulesByEmp("E001");
            return View(query);
        }

        public ActionResult Planning()
        {
            return View();
        }
    }
}