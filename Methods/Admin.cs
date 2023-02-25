using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class Admin : UserManage
    {
        public override UserManage GetUser()
        {
            Role = RoleType.Admin;
            return new Admin();
        }

        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.Employees.Where(w => w.EId != CurrentUser.EId).OrderBy(o => o.StartDate).ThenBy(t => !t.Allow).ToListAsync();

                List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
                foreach (var emp in query)
                {
                    viewModels.Add(EmployeeUtility.RefEmployee(emp));
                }
                return viewModels;
            }
        }
    }
}


