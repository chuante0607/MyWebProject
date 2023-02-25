using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;
namespace UCOMProject.Methods
{
    public class Manager : UserManage
    {
        public Manager(BranchType branchType)
        {
            BranchType = branchType;
        }

        public override UserManage GetUser()
        {
            Role = RoleType.Manager;
            return new Manager(BranchType);
        }

        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.Employees.Where(w => w.Branch == BranchType.ToString()).ToListAsync();

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