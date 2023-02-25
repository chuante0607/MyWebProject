using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{


    public abstract class UserManage
    {
        protected Employee CurrentUser { get { return SessionEmp.CurrentEmp; } }
        protected BranchType BranchType { get; set; }
        protected RoleType Role { get; set; }
   
        /// <summary>
        /// 取得目前使用者的實例對象
        /// </summary>
        /// <returns></returns>
        public abstract UserManage GetUser();

        /// <summary>
        /// 取得所有員工資訊
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<EmployeeViewModel>> GetEmployees();

        /// <summary>
        /// 取得指定員工資訊
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

       
    }
}