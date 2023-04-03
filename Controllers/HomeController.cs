using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        public SummaryViewModel vm = new SummaryViewModel();
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        public async Task<ActionResult> Index(int? year)
        {
            

            RoleManage user = ConfirmIdentity();
            List<HolidayDetailViewModel> details = new List<HolidayDetailViewModel>();
            List<EmployeeViewModel> emps = new List<EmployeeViewModel>();
            //To do List事項提示功能
            if (user.GetRole() != RoleType.User)
            {
                //主管---休假審核提示
                var query = await user.GetHolidayDetails();
                details = query.Where(d => d.State == 1).ToList();
                emps = await user.GetEmployees();
                Session["emps"] = emps;
            }
            else
            {
                //員工---休假退回提示
                var query = await user.GetHolidayDetails();
                details = query.Where(d => d.State == 3).ToList();
            }
            Session["detail"] = details;

            //計算個人年資seniority
            DateTime empStartDate = SessionEmp.CurrentEmp.StartDate;
            TimeSpan span = DateTime.Now.Subtract(empStartDate);
            int totalDays = span.Days;

            decimal totalYears = Math.Truncate(((decimal)totalDays / 365) * 100) / 100;
            decimal totalMonths = 0m;
            string seniority = "";
            if (totalYears < 1)
            {
                totalMonths = Math.Truncate(totalYears * 365) / 30;
                totalMonths = Math.Truncate(totalMonths * 10) / 10;
                seniority = $"{totalMonths}個月";
            }
            else
            {
                totalMonths = Math.Truncate((totalYears % (int)totalYears) * 365) / 30;
                totalMonths = Math.Truncate(totalMonths * 10) / 10;
                seniority = $"{(int)totalYears}年{totalMonths}個月";
            }
            Session["seniority"] = seniority;

            //加班需求通知
            List<OverTimeDetail> overDetails = await ScheduleUtility.GetOverTimeDetails(user.CurrentUser.EId);
            if (overDetails != null && overDetails.Count > 0)
            {
                ViewBag.overTime = JsonConvert.SerializeObject(overDetails);
            }
            else
            {
                ViewBag.overTime = 0;
            }

            //vm.Attendance = await ScheduleUtility.GetAttendance();
            year = year == null ? DateTime.Now.Year : year;
            vm.Employee = SessionEmp.CurrentEmp;
            if (vm.Employee.EId == "admin") RedirectToAction("login", "admin");
            vm.Holidays = await HolidayUtility.GetHolidayInfosByEmp(vm.Employee.EId);
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

        public ActionResult Chart()
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

