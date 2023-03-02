using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;
using UCOMProject.Methods;
using UCOMProject.Interfaces;

namespace UCOMProject.Roles
{
    public class Manager : RoleManage, IHolidayReviewer
    {
        public Manager(RoleType role) : base(role) { }

        public override RoleManage GetRole()
        {
            return new Manager(RoleType.Manager);
        }

        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            //manager可以查看自己部門的員工資訊
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
            return await HolidayUtility.GetHolidayDetails(BranchType);
        }

        public async Task<bool> Review(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state , CurrentUser);
        }
    }
}