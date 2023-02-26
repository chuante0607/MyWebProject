using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Extension;
using UCOMProject.Models;

namespace UCOMProject.Methods
{


    public abstract class UserManage
    {
        protected Employee CurrentUser { get { return SessionEmp.CurrentEmp; } }
        protected BranchType BranchType { get { return CurrentUser.Branch.xTranBranchEnum(); } }
        protected RoleType Role { get; set; }
        public UserManage(RoleType role)
        {
            Role = role;
        }

        /// <summary>
        /// 取得員工資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public virtual async Task<EmployeeViewModel> GetEmployeeById(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee result = await db.Employees.FindAsync(eid);
                if (result == null) return null;
                EmployeeViewModel vmEmp = EmployeeUtility.RefEmployee(result);
                return vmEmp;
            }
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
    }
}