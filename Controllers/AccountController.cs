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
    public class AccountController : Controller
    {

        [AllowAnonymous]
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="logout"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.login = 0;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string id, string pwd)
        {
            if (id.ToLower() == "admin")
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "此為管理員限定帳號" });
                return View();
            }
            Employee employee = await EmployeeUtility.MatchUser(id, pwd);
            if (employee == null)
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號密碼錯誤!" });
                return View();
            }
            if (employee.Allow)
            {
                Session["emp"] = employee;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "待管理員開通帳號權限!" });
                return View();
            }
        }

        [AuthorizationFilter]
        public ActionResult Logout()
        {
            HttpContext.Session["emp"] = null;
            return RedirectToAction("login", "account", new { logout = true, msg = "已登出!" });
        }

    }
}