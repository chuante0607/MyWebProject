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


        /// <summary>
        /// user的實體
        /// </summary>
        /// <returns>User</returns>
        public override RoleType GetRole()
        {
            return RoleType.User;
        }

        /// <summary>
        /// 員工權限只能取的本身的資料
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>EmployeeViewModel</returns>
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

        /// <summary>
        /// user權限無法獲得員工資訊
        /// </summary>
        /// <return>throw Employee不可查詢所有員工資訊</return>
        public override Task<List<EmployeeViewModel>> GetEmployees()
        {
            //employee無法查詢其他員工資訊
            throw new Exception("Employee不可查詢所有員工資訊");
        }

        /// <summary>
        /// 取得自己的休假紀錄
        /// </summary>
        /// <returns>List<HolidayDetailViewModel></returns>
        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails(CurrentUser.EId);
        }

        /// <summary>
        /// 查詢自己的休假天數資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>List<HolidayViewModel>></returns>
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

        public override Task<List<AttendanceViewModel>> GetAttendances(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}