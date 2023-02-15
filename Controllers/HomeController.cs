using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        public  HomeViewModel vm = new HomeViewModel();
        public HomeController()
        {
            if (SessionEmp.CurrentEmp != null)
            {
                vm.Employee = SessionEmp.CurrentEmp;
                vm.Holidays = HolidayUtility.getCanUseHolidays(vm.Employee.EId);
                vm.ChartDict = HolidayUtility.getchartDictByEmpID(vm.Employee.EId);
            }
        }

    
        public ActionResult Index()
        {
            ViewBag.Source = JsonConvert.SerializeObject(vm , new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()} );
            return View();
        }
    }
}