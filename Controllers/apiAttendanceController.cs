using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.API;
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiAttendanceController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
       
        
        public async Task<IHttpActionResult> Get(DateTime? date)
        {
            var head = Request.Headers.Select(s => new { s.Key, s.Value }).FirstOrDefault(w => w.Key == "Authorization");
            string eid = "";
            foreach (var id in head.Value)
            {
                eid = id;
            }
            try
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    Employee emp = await db.Employees.FirstOrDefaultAsync(e => e.EId == eid);
                    if (emp == null)
                        throw new Exception("無目前使用者資訊");
                    RoleManage user = ConfirmIdentity(emp.JobRank, emp.Branch.xTranBranchEnum());
                    DateTime currentDate = (DateTime)date;
                    List<AttendanceViewModel> attendances = await user.GetAttendances(currentDate);
                    var query = attendances.Skip(5).Take(10);
                    return Json(new { success = true, attendances = attendances });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
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
        // GET: api/apiAttendance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/apiAttendance
        public async Task<IHttpActionResult> Post([FromBody] DateTime? date)
        {
            var head = Request.Headers.Select(s => new { s.Key, s.Value }).FirstOrDefault(w => w.Key == "Authorization");
            string eid = "";
            foreach (var id in head.Value)
            {
                eid = id;
            }
            try
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    Employee emp = await db.Employees.FirstOrDefaultAsync(e => e.EId == eid);
                    if (emp == null)
                        throw new Exception("無目前使用者資訊");
                    RoleManage user = ConfirmIdentity(emp.JobRank, emp.Branch.xTranBranchEnum());
                    DateTime currentDate = (DateTime)date;
                    List<AttendanceViewModel> attendances = await user.GetAttendances(currentDate);
                    return Json(new { success = true, attendances = attendances });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        // PUT: api/apiAttendance/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/apiAttendance/5
        public void Delete(int id)
        {
        }
    }
}
