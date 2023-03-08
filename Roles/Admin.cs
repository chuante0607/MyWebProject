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
    public class Admin : RoleManage, IHolidayReviewer, IAccountReviewer
    {
        public Admin(RoleType role) : base(role) { }

        /// <summary>
        /// Admin實體
        /// </summary>
        /// <returns>Admin</returns>
        public override RoleType GetRole()
        {
            return RoleType.Admin;
        }

        /// <summary>
        /// 獲取全部員工資訊
        /// </summary>
        /// <returns>EmployeeViewModel集合</returns>
        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            return await EmployeeUtility.GetEmployees();
        }

        /// <summary>
        /// 獲取全部休假紀錄
        /// </summary>
        /// <returns>HolidayDetailViewModel集合</returns>
        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails();
        }

        /// <summary>
        /// 審核所有休假單
        /// </summary>
        /// <param name="data"></param>
        /// <param name="state"></param>
        /// <returns>bool</returns>
        public async Task<bool> ReviewHolidayApply(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state, CurrentUser);
        }

        /// <summary>
        /// 設定權限
        /// </summary>
        /// <param name="emps"></param>
        /// <returns>bool</returns>
        public async Task<bool> SetAccountRole(List<EmployeeViewModel> emps)
        {
            try
            {
                foreach (EmployeeViewModel emp in emps)
                {
                    await EmployeeUtility.UpdateEmpInfo(emp);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 查詢所有員工的休假天數
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>HolidayViewModel集合</returns>
        public override async Task<List<HolidayViewModel>> GetHolidayInfosByEmp(string eid)
        {
            return await HolidayUtility.GetHolidayInfosByEmp(eid);
        }
    }
}


