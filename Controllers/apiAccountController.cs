using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.Interfaces;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;

namespace UCOMProject.Controllers
{
    public class apiAccountController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IHttpActionResult Get(List<int> ids)
        {
            return Ok();
        }

        // POST: api/apiAccount
        public async Task<IHttpActionResult> PostAsync([FromBody] List<EmployeeViewModel> emps, string rank, BranchType branch)
        {
            bool check = int.TryParse(rank, out int checkRank);
            if (!check)
                return NotFound();
            IAccountReviewer reviewer = ConfirmIAccountReviewer(checkRank);
            if (reviewer == null)
                return NotFound();
            try
            {
                if (await reviewer.SetAccountRole(emps))
                {
                    RoleManage user = ConfirmIdentity(checkRank, branch);
                    List<EmployeeViewModel> vm = await user.GetEmployees();
                    return Ok(JsonConvert.SerializeObject(vm, camelSetting));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return NotFound();
            }
        }

        private IAccountReviewer ConfirmIAccountReviewer(int rank)
        {
            switch (rank)
            {
                case 0:
                    return new Admin(RoleType.Admin);
                case 2:
                    return new Manager(RoleType.Manager);
            }
            return null;
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
