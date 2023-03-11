using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Extension;
using UCOMProject.Models;
using UCOMProject.Methods;

namespace UCOMProject.Roles
{


    public abstract class RoleManage
    {
        protected Employee CurrentUser { get { return SessionEmp.CurrentEmp; } }
        protected BranchType BranchType { get; set; }
        public RoleType Role { get; set; }
        public RoleManage(RoleType role)
        {
            Role = role;
        }

        public RoleManage(RoleType role, BranchType branch)
        {
            BranchType = branch;
            Role = role;
        }
        /// <summary>
        /// 取得目前使用者身分
        /// </summary>
        /// <returns></returns>
        public abstract RoleType GetRole();

        /// <summary>
        /// 取得目前使用者的資訊
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeeViewModel> GetUser()
        {
            return await EmployeeUtility.GetEmpById(CurrentUser.EId);
        }

        /// <summary>
        /// 員工修改自己的資訊
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        public async Task<ApplyResult> EditUserInfo(EditEmployeeViewModel edit)
        {
            try
            {
                ApplyResult result = await EmployeeUtility.UpdateSelfInfo(edit);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 取得員工資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public virtual async Task<EmployeeViewModel> GetEmployeeById(string eid)
        {
            EmployeeViewModel emp = await EmployeeUtility.GetEmpById(eid);
            if (emp == null) return null;
            return emp;
        }

        /// <summary>
        /// 取得所有員工資訊
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<EmployeeViewModel>> GetEmployees();



        /// <summary>
        /// 取得休假資訊
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<HolidayDetailViewModel>> GetHolidayDetails();

        /// <summary>
        /// 查詢員工休假資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public abstract Task<List<HolidayViewModel>> GetHolidayInfosByEmp(string eid);

        /// <summary>
        /// 刪除員工自己待審核的休假申請
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelHolidayDetails(List<int> id)
        {
            return await HolidayUtility.DelHolidayDetails(id, CurrentUser.EId);
        }

        /// <summary>
        /// 依4班2輪類型取得年度工作日
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<List<ShiftViewModel>> GetWorkDayOfYearByMonth(ShiftType shift, int year)
        {
            return ScheduleUtility.GetWorkDayOfYearByMonth(shift, year);
        }

        /// <summary>
        /// 常日班取得年度工作日
        /// </summary>
        /// <param name="file"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<List<ShiftViewModel>> GetWorkDayOfYearByMonth(string[] file, int year)
        {
            return ScheduleUtility.GetWorkDayOfYearByMonth(file, year);
        }

    }
}