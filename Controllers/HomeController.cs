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

        public async Task<ActionResult> Index()
        {
            vm.Employee = SessionEmp.CurrentEmp;
            vm.Holidays = await HolidayUtility.GetHolidayInfos(vm.Employee.EId);
            vm.ChartInfos = await HolidayUtility.GetchartInfos(vm.Employee.EId);
            try
            {
            ViewBag.Source = JsonConvert.SerializeObject(vm, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            }catch(ObjectDisposedException ex)
            {
               string msg =  ex.Message;
            }
            return View();
        }
    }
}