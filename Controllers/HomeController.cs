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
        private readonly HomeViewModel _vm = new HomeViewModel();
        public HomeController()
        {
            if (SessionEmp.CurrentEmp != null)
            {
                _vm.Employee = SessionEmp.CurrentEmp;
                _vm.Holidays = HolidayUtility.getCanUseHolidaysByEmpID(_vm.Employee.EId);
                _vm.ChartDict = HolidayUtility.getchartDictByEmpID(_vm.Employee.EId);
            }
        }

    
        public ActionResult Index()
        {
            ViewBag.Source = JsonConvert.SerializeObject(_vm , new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View();
        }
    }
}