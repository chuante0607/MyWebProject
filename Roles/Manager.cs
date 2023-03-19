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
    public class Manager : RoleManage, IHolidayReviewer, IAccountReviewer
    {
        public Manager(RoleType role) : base(role) { }
        public Manager(RoleType role, BranchType branch) : base(role, branch) { }


        public override RoleType GetRole()
        {
            return RoleType.Manager;
        }

        /// <summary>
        /// 獲取部門的員工資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>EmployeeViewModel</returns>
        public override async Task<EmployeeViewModel> GetEmployeeById(string eid)
        {
            //主管只能查自己部門員工的假別
            using (MyDBEntities db = new MyDBEntities())
            {
                int branchId = db.Employees.Find(eid).BranchId;
                if (CurrentUser.BranchId == branchId)
                {
                    EmployeeViewModel emp = await EmployeeUtility.GetEmpById(eid);
                    return emp;
                }
                else
                {
                    throw new Exception("部門主管只能查直屬部門員工資訊");
                }
            }

        }

        /// <summary>
        /// 獲取部門的所有員工資訊
        /// </summary>
        /// <returns>EmployeeViewModel集合</returns>
        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            return await EmployeeUtility.GetEmployees(new List<int> { (int)BranchType });
        }

        /// <summary>
        /// 獲取部門的休假紀錄
        /// </summary>
        /// <returns>HolidayDetailViewModel集合</returns>
        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails(BranchType);
        }

        /// <summary>
        /// 審核部門的休假單
        /// </summary>
        /// <param name="data"></param>
        /// <param name="state"></param>
        /// <returns>bool</returns>
        public async Task<bool> ReviewHolidayApply(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state, CurrentUser);
        }

        /// <summary>
        /// 查詢自己所屬部門員工的休假天數資訊
        /// </summary>
        /// <param name="eid"></param>
        /// <returns>HolidayViewModel集合</returns>
        public override async Task<List<HolidayViewModel>> GetHolidayInfosByEmp(string eid)
        {
            //主管只能查自己部門員工的假別
            using (MyDBEntities db = new MyDBEntities())
            {
                int branchId = db.Employees.Find(eid).BranchId;
                if (CurrentUser.BranchId == branchId)
                {
                    return await HolidayUtility.GetHolidayInfosByEmp(eid);
                }
                else
                {
                    return new List<HolidayViewModel>();
                }
            }
        }

        /// <summary>
        /// 設定部門的權限
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

        public override async Task<List<AttendanceViewModel>> GetAttendances(DateTime date)
        {
            try
            {
                return await ScheduleUtility.GetAttendances(date, BranchType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}