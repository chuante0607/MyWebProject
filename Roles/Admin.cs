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
    public class Admin : RoleManage, IHolidayReview
    {
        public Admin(RoleType role) : base(role) { }

        public override async Task<List<EmployeeViewModel>> GetEmployees()
        {
            //admin可以查詢所有部門員工資訊
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.Employees.Where(w => w.EId != CurrentUser.EId).OrderBy(o => o.StartDate).ThenBy(t => !t.Allow).ToListAsync();
                List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
                foreach (var emp in query)
                {
                    viewModels.Add(EmployeeUtility.RefEmployee(emp));
                }
                return viewModels;
            }
        }

        public override async Task<List<HolidayDetailViewModel>> GetHolidayDetails()
        {
            return await HolidayUtility.GetHolidayDetails();
        }

        public async Task<bool> Review(List<HolidayDetailViewModel> data, ReviewType state)
        {
            return await HolidayUtility.EditHolidayDetailsState(data, state);
        }
    }
}


