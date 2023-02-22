using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class EmployeeUtility
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static async Task<Employee> GetEmp(string id, string pwd)
        {
            using (MyDBEntities db = new MyDBEntities())
            {

                return await db.Employees.SingleOrDefaultAsync(e => e.EId == id && e.Password == pwd);
            }
        }

        /// <summary>
        /// 取得指定員工ViewMode
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static async Task<EmployeeViewModel> GetEmp(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = await db.Employees.FindAsync(eid);
                EmployeeViewModel vmEmp = new EmployeeViewModel();
                //將DB的employee轉換至employeeModel
                if (emp == null)
                    return null;
                Type typeA = emp.GetType();
                Type typeB = vmEmp.GetType();
                PropertyInfo[] props = typeA.GetProperties();
                foreach (PropertyInfo proA in props)
                {
                    PropertyInfo proB = typeB.GetProperty(proA.Name);
                    if (proB != null)
                    {
                        proB.SetValue(vmEmp, proA.GetValue(emp));
                    }
                }
                return vmEmp;
            }
        }

        /// <summary>
        /// 取得指定班別的所有員工
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static async Task<List<EmployeeViewModel>> GetEmpsByShift(ShiftType shift)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var emps = await db.Employees.Where(w => w.Shift == shift.ToString()).ToListAsync();
                List<EmployeeViewModel> vm = new List<EmployeeViewModel>();
                foreach (var emp in emps)
                {
                    EmployeeViewModel vmEmp = new EmployeeViewModel();
                    Type typeA = emp.GetType();
                    Type typeB = vmEmp.GetType();
                    PropertyInfo[] props = typeA.GetProperties();
                    foreach (PropertyInfo proA in props)
                    {
                        PropertyInfo proB = typeB.GetProperty(proA.Name);
                        if (proB != null)
                        {
                            proB.SetValue(vmEmp, proA.GetValue(emp));
                        }
                    }
                    vm.Add(vmEmp);
                }
                return vm;
            }
        }

        /// <summary>
        /// 建立員工資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static (bool result, string msg, string fileName) CreateEmpInfo(EmployeeViewModel vm)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var emp = db.Employees.Find(vm.EId);
                if (emp != null)
                    return (false, $"{vm.EId}已存在,請勿使用", null);

                string fileName = $"{DateTime.Now.Ticks}{vm.Image.FileName}";
                emp = new Employee
                {
                    EId = vm.EId,
                    Password = vm.Password,
                    Name = vm.Name,
                    EnglishName = vm.EnglishName,
                    Sex = vm.Sex,
                    Branch = vm.Branch,
                    JobTitle = vm.JobTitle,
                    JobRank = vm.JobRank,
                    Email = vm.Email,
                    Phone = vm.Phone,
                    Shift = vm.Shift.ToString(),
                    StartDate = vm.StartDate,
                    Image = fileName,
                    Allow = false,
                };

                db.Employees.Add(emp);
                db.SaveChanges();
                return (true, $"單位:{emp.Branch}\r\n工號:{emp.EId} ,姓名${emp.Name}帳號已開通!", fileName);
            }
        }

    }
}