using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;
using UCOMProject.Methods;
using UCOMProject.Interfaces;
using UCOMProject.Extension;

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
            return await EmployeeUtility.GetEmployees(new List<int> { (int)BranchType });
        }

        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails(BranchType);
        }

        public async Task<bool> ReviewHolidayApply(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state, CurrentUser);
        }
    }
}