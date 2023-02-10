using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 登入 (我是更新) (製造衝突)
        /// </summary>
        /// <param name="logout"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ActionResult Login(bool? logout, string msg)
        {
            ViewBag.status = JsonConvert.SerializeObject(new { logout = logout, msg = msg });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string id, string pwd)
        {
            EmployeeModel employee = EmployeeUtility.getEmp(id, pwd);
            if (employee == null)
            {
                ViewBag.status = JsonConvert.SerializeObject(new { logout = false, msg = "帳號密碼錯誤!" });
                return View();
            }
            Session["emp"] = employee;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session["emp"] = null;
            return RedirectToAction("Login", new { logout = true, msg = "已登出!" });
        }

    }
}