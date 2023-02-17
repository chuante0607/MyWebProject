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
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="logout"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string id, string pwd)
        {
            Employee employee = await EmployeeUtility.GetEmp(id, pwd);
            if (employee == null)
            {
                ViewBag.status = JsonConvert.SerializeObject(new { logout = false, msg = "帳號密碼錯誤!" });
                return View();
            }
            Session["emp"] = employee;
            return RedirectToAction("Index", "Home");
        }

        [AuthorizationFilter]
        public ActionResult Logout()
        {
            HttpContext.Session["emp"] = null;
            return RedirectToAction("Login", new { logout = true, msg = "已登出!" });
        }

    }
}