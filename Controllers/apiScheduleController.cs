using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.API;
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiScheduleController : ApiController
    {

        //主管取得部門或全員工休假總覽
        public async Task<IHttpActionResult> Get()
        {
            var head = Request.Headers.Select(s => new { s.Key, s.Value }).FirstOrDefault(w => w.Key == "Authorization");
            string eid = "";
            foreach (var id in head.Value)
            {
                eid = id;
            }
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = await db.Employees.FirstOrDefaultAsync(e => e.EId == eid);
                if (emp == null)
                {
                    return Json(new { success = false, msg = "無目前使用者資訊" });
                }
                RoleManage user = ConfirmIdentity(emp.JobRank, emp.Branch.xTranBranchEnum());
                user.CurrentUser = emp;
                ScheduleApiModel schedule = await ScheduleUtility.GetSchedule(user);
                schedule.plansByDay = await ScheduleUtility.GetPlansByDay();
                return Json(schedule);
            }
        }

        //取得自己的休假總覽
        public async Task<IHttpActionResult> Get(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = await db.Employees.FirstOrDefaultAsync(e => e.EId == eid);
                if (emp == null)
                {
                    return Json(new { success = false, msg = "無目前使用者資訊" });
                }
                RoleManage user = new User(RoleType.User);
                user.CurrentUser = emp;
                ScheduleApiModel schedule = await ScheduleUtility.GetSchedule(user);
                schedule.plansByDay = await ScheduleUtility.GetPlansByDay();
                return Json(schedule);
            }
        }

        private RoleManage ConfirmIdentity(int rank, BranchType branch)
        {
            RoleManage user = null;
            switch (rank)
            {
                case 0:
                    user = new Admin(RoleType.Admin);
                    break;
                case 1:
                    user = new User(RoleType.User);
                    break;
                case 2:
                    user = new Manager(RoleType.Manager, branch);
                    break;
            }
            return user;
        }



        public async Task<IHttpActionResult> Post([FromBody] OverTimeDetail data)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.OverTimeDetails
                    .FirstOrDefaultAsync(f => f.OverTimeDate == data.OverTimeDate && f.EId == data.EId);
                if (query != null)
                {
                    query.UserCheck = data.UserCheck;
                    await db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, msg = "查無加班通知!請與部門主管確認" });
                }
            }
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
