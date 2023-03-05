using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.Interfaces;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiAccountController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        // GET: api/apiAccount
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/apiAccount/5s
        public IHttpActionResult Get(List<int> ids)
        {
            return Ok();
        }

        // POST: api/apiAccount
        public async Task<IHttpActionResult> PostAsync([FromBody] List<EmployeeViewModel> emps)
        {
            Admin admin = new Admin(RoleType.Admin);
            if (await admin.SetAccountRole(emps))
            {
                List<EmployeeViewModel> vm = await admin.GetEmployees();
                return Ok(JsonConvert.SerializeObject(vm, camelSetting));
            }
            else
            {
                return NotFound();
            }
            //Admin admin = new Admin(RoleType.Admin);
            //List<EmployeeViewModel> result = null;
            ////前端過來為"全選"
            //if (ids.Contains(0))
            //{
            //    result = await admin.GetEmployees();
            //    return Ok(JsonConvert.SerializeObject(result, camelSetting));
            //}
            ////前端指定部門
            //result = await admin.GetEmployeesByBranch(ids);
            //if (result == null)
            //    return NotFound();
            //else
            //    return Ok(JsonConvert.SerializeObject(result, camelSetting));
        }

        // PUT: api/EmpList/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/EmpList/5
        public void Delete(int id)
        {
        }
    }
}
