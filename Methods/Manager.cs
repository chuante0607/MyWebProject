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
        public Manager(RoleType role) : base(role) { }

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

        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetailList(BranchType);
        }
    }
}