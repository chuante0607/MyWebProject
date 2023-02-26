using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.login = 0;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string id, string pwd)
        {
            if (id.ToLower() == "admin" && pwd.ToLower() == "admin")
            {
                Employee admin = await EmployeeUtility.GetEmp(id, pwd);
                if (admin == null)
                {
                    ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號密碼無效" });
                    return View();
                }
                Session["emp"] = admin;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.login = JsonConvert.SerializeObject(new { error = true, msg = "帳號密碼無效" });
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["emp"] = null;
            return RedirectToAction("login", "admin");
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            UserManage user = new Admin(RoleType.Admin);
            var emps = await user.GetEmployees();
            return View(emps);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
