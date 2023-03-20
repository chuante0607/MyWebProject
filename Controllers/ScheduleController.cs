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
using System.Globalization;
using UCOMProject.Extension;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class ScheduleController : Controller
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        //User主管與管理員的頁面
        public ActionResult Index()
        {
            return View();
        }

        //User使用的頁面
        public ActionResult IndexByUser()
        {
            return View();
        }

        public async Task<ActionResult> Plan()
        {
            ViewBag.Source = JsonConvert.SerializeObject(await EmployeeUtility.GetEmployees(), camelSetting);
            return View();
        }

        public async Task<ActionResult> AttendanceDay(DateTime? date)
        {
            //const decimal totalPages = 10;
            try
            {
                DateTime currentDate = (DateTime)date;
                RoleManage user = ConfirmIdentity();
                List<AttendanceViewModel> attendances = await user.GetAttendances(currentDate);
                ViewBag.source = JsonConvert.SerializeObject(attendances);
                ViewBag.viewDate = currentDate.ToLongDateString();
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "NotFound");
            }

        }


        [HttpPost]
        public async Task<ActionResult> AttendanceDay(EmployeeViewModel emp , DateTime date)
        {

            return View();
            //const decimal totalPages = 10;
            //try
            //{
            //    DateTime currentDate = (DateTime)date;
            //    RoleManage user = ConfirmIdentity();
            //    List<AttendanceViewModel> attendances = await user.GetAttendances(currentDate);
            //    ViewBag.source = JsonConvert.SerializeObject(attendances);
            //    ViewBag.viewDate = currentDate.ToLongDateString();
            //    return View();
            //}
            //catch
            //{
            //    return RedirectToAction("Index", "NotFound");
            //}

        }




        public async Task<ActionResult> NotifyDay(DateTime? date)
        {
            
            RoleManage user = ConfirmIdentity();
            return View();
        }

        public async Task<ActionResult> Notify(DateTime? date)
        {

            RoleManage user = ConfirmIdentity();

            List<EmployeeViewModel> empss = await user.GetEmployees();

            NotifyViewModel notify = new NotifyViewModel();

            //int needNum = Math.Abs(int.Parse(need));
            DateTime checkDate = (DateTime)date;
            if (checkDate.Year == DateTime.Now.Year)
            {
                ShiftType type = await ScheduleUtility.GetShiftTypeByDate(checkDate, checkDate.Year);
                var emps = await EmployeeUtility.GetEmployees();
                List<EmployeeViewModel> employees = emps.Where(e => e.ShiftType == type).ToList();
                return View(employees);
            }
            return RedirectToAction("index", "NotFound");
        }

        public ActionResult Event()
        {
            return View();
        }

        private RoleManage ConfirmIdentity()
        {
            RoleManage user = null;
            switch (SessionEmp.CurrentEmp.JobRank)
            {
                case 0:
                    user = new Admin(RoleType.Admin);
                    break;
                case 1:
                    user = new User(RoleType.User);
                    break;
                case 2:
                    user = new Manager(RoleType.Manager, SessionEmp.CurrentEmp.Branch.xTranBranchEnum());
                    break;
            }
            return user;
        }
    }
}