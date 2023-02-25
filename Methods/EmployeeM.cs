using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class EmployeeM : UserManage
    {
        public override UserManage GetUser()
        {
            Role = RoleType.Employee;
            return new EmployeeM();
        }

        public override Task<List<EmployeeViewModel>> GetEmployees()
        {
            throw new Exception("Admin與Manager才可查詢所有員工資訊");
        }

        public override Task<EmployeeViewModel> GetEmployeeById(string eid)
        {
            if (eid == CurrentUser.EId)
                return base.GetEmployeeById(eid);
            else
            {
                throw new Exception("Admin與Manager才可查詢其員工資訊");
            }
        }
    }
}