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
    public class Admin : RoleManage, IHolidayReviewer, IAccountReviewer, IEmployeeList
    {
        public Admin(RoleType role) : base(role) { }

        public override RoleManage GetRole()
        {
            return new Admin(RoleType.Admin);
        }

        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            return await EmployeeUtility.GetEmployees();
        }

        public async Task<List<EmployeeViewModel>> GetEmployeesByBranch(List<int> branchId)
        {
            return await EmployeeUtility.GetEmployees(branchId);
        }

        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails();
        }

        public async Task<bool> ReviewHolidayApply(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state, CurrentUser);
        }

        public async Task<bool> ReviewEmployeeAccount(string eid)
        {
            return await EmployeeUtility.ReviewEmployeeAccount(eid);
        }

        public async Task<bool> ReviewEmployeeAccount(List<string> eid)
        {
            throw new NotImplementedException();
        }

    }
}


