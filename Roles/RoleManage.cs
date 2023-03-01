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
        protected BranchType BranchType { get { return CurrentUser.Branch.xTranBranchEnum(); } }
        public RoleType Role { get; set; }
        public RoleManage(RoleType role)
        {
            Role = role;
        }
        /// <summary>
        /// 取得目前使用者的資訊
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeeViewModel> GetUser()
        {
            return await EmployeeUtility.GetEmpById(CurrentUser.EId);
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
        /// 刪除員工自己待審核的休假申請
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelHolidayDetails(List<int> id)
        {
            return await HolidayUtility.DelHolidayDetails(id , CurrentUser.EId);
        }
    }
}