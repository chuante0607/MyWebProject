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

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        public SummaryViewModel vm = new SummaryViewModel();
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        public async Task<ActionResult> Index(int? year)
        {
            year = year == null ? DateTime.Now.Year : year;
            vm.Employee = SessionEmp.CurrentEmp;
            if (vm.Employee.EId == "admin") RedirectToAction("login", "admin");
            vm.Holidays = await HolidayUtility.GetHolidayInfos(vm.Employee.EId);
            vm.ChartInfos = await HolidayUtility.GetchartInfos(vm.Employee.EId, (int)year);
            try
            {
                ViewBag.Source = JsonConvert.SerializeObject(vm, camelSetting);
            }
            catch (ObjectDisposedException ex)
            {
                string msg = ex.Message;
            }
            return View();
        }
    }
}

