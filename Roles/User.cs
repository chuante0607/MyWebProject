using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;
using UCOMProject.Methods;

namespace UCOMProject.Roles
{
    public class User : RoleManage
    {
        public User(RoleType role) : base(role) { }

        public override RoleManage GetRole()
        {
            return new User(RoleType.User);
        }

        public override Task<EmployeeViewModel> GetEmployeeById(string eid)
        {
            //employee只能查詢自己的資訊
            if (eid == CurrentUser.EId)
                return base.GetEmployeeById(eid);
            else
            {
                throw new Exception("Employee只可查詢自己的資訊");
            }
        }

        public override Task<List<EmployeeViewModel>> GetEmployees()
        {
            //employee無法查詢其他員工資訊
            throw new Exception("Employee不可查詢所有員工資訊");
        }

        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails(CurrentUser.EId);
        }
        public override async Task<List<HolidayViewModel>> GetHolidayInfosByEmp(string eid)
        {
            //員工只能查自己的假別
            if (CurrentUser.EId == eid)
            {
                return await HolidayUtility.GetHolidayInfosByEmp(eid);
            }
            else
            {
                return new List<HolidayViewModel>();
            }
        }
    }
}