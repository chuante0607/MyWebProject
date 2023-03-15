using Newtonsoft.Json;
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
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.login = 0;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string eid, string pwd)
        {
            Employee employee = await EmployeeUtility.MatchUser(eid, pwd);
            if(employee == null)
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號密碼錯誤!" });
            }
            else
            {
                Session["emp"] = employee;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult AdminLogin()
        {
            ViewBag.login = 0;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AdminLogin(string id, string pwd)
        {
            Employee admin = await EmployeeUtility.MatchUser(id, pwd);
            if (admin == null)
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號密碼無效!" });
                return View();
            }
            if (admin.JobRank == 0)
            {
                Session["emp"] = admin;
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號權限需為管理員身分!" });
                return View();
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session["emp"] = null;
            return RedirectToAction("login", "account", new { logout = true, msg = "已登出!" });
        }
    }
}