using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UCOMProject.Models;
using UCOMProject.Extension;

namespace UCOMProject.Methods
{
    public class EmployeeUtility
    {
        /// <summary>
        /// DataBaseEmp to ViewModel
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public static EmployeeViewModel RefEmployee(Employee emp)
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
            vmEmp.BranchType = emp.Branch.xTranBranchEnum();
            vmEmp.JobType = emp.JobTitle.xTranJobTitleEnum();
            vmEmp.ShiftType = emp.Shift.xTranShiftEnum();
            vmEmp.SexType = emp.Sex.xTranSexBool();
            return vmEmp;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static async Task<Employee> MatchUser(string id, string pwd)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                return await db.Employees.SingleOrDefaultAsync(e => e.EId == id && e.Password == pwd);
            }
        }

        /// <summary>
        /// 取得所有員工資料
        /// </summary>
        /// <returns></returns>
        public static async Task<List<EmployeeViewModel>> GetEmployees()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.Employees.OrderBy(o => o.EId).ToListAsync();
                List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
                foreach (var emp in query)
                {
                    viewModels.Add(RefEmployee(emp));
                }
                return viewModels;
            }
        }

        /// <summary>
        /// 取得部門員工資料
        /// </summary>
        /// <returns></returns>
        public static async Task<List<EmployeeViewModel>> GetEmployees(List<int> branchIds)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var query = await db.Employees.Where(w => branchIds.Contains(w.BranchId))
                    .OrderBy(o => o.EId).ToListAsync();
                if (query.Count > 0)
                {
                    List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
                    foreach (var emp in query)
                    {
                        viewModels.Add(RefEmployee(emp));
                    }
                    return viewModels;
                }
                return null;
            }
        }

        /// <summary>
        /// 取得指定員工
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static async Task<EmployeeViewModel> GetEmpById(string eid)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = await db.Employees.FindAsync(eid);
                //將DB的employee轉換至employeeModel
                if (emp == null)
                    return null;
                return RefEmployee(emp);
            }
        }


        /// <summary>
        /// 建立員工資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplyResult CreateEmpInfo(EmployeeViewModel vm)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var emp = db.Employees.Find(vm.EId);
                if (emp != null)
                    return new ApplyResult
                    {
                        isPass = false,
                        msg = $"工號:{vm.EId}已存在,請勿使用"
                    };

                string fileName = $"{DateTime.Now.Ticks}{vm.ImageFile.FileName}";
                emp = new Employee
                {
                    EId = vm.EId,
                    Password = vm.Password,
                    Name = vm.Name,
                    EnglishName = vm.EnglishName,
                    Sex = vm.Sex,
                    Branch = vm.Branch,
                    BranchId = vm.BranchId,
                    JobTitle = vm.JobTitle,
                    JobRank = 1,
                    Email = vm.Email,
                    Phone = vm.Phone,
                    Shift = vm.Shift.ToString(),
                    StartDate = vm.StartDate,
                    Image = fileName,
                    Allow = false,
                };

                db.Employees.Add(emp);
                db.SaveChanges();
                return new ApplyResult
                {
                    isPass = true,
                    msg = $"單位:{emp.Branch}\r\n工號:{emp.EId} ,姓名:{emp.Name}\r\n資料已送出,待管理員審核!",
                    FileName = fileName
                };
            }
        }

        /// <summary>
        /// 修改員工資料
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static async Task UpdateEmpInfo(EmployeeViewModel vm)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = await db.Employees.FindAsync(vm.EId);
                if (emp == null)
                {
                    throw new Exception($"找不到工號{vm.EId}員工資訊");
                }
                emp.Password = vm.Password;
                emp.Name = vm.Name;
                emp.EnglishName = vm.EnglishName;
                emp.Sex = vm.Sex;
                emp.Branch = vm.Branch;
                emp.BranchId = vm.BranchId;
                emp.JobTitle = vm.JobTitle;
                emp.JobRank = vm.JobRank;
                emp.Email = vm.Email;
                emp.Phone = vm.Phone;
                emp.Shift = vm.Shift.ToString();
                emp.StartDate = vm.StartDate;
                emp.Image = vm.Image;
                emp.Allow = vm.Allow;
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 修改自己的資訊
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static async Task<ApplyResult> UpdateSelfInfo(EditEmployeeViewModel vm)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                ApplyResult result = new ApplyResult();
                Employee emp = await db.Employees.FindAsync(vm.EId);
                if (emp == null)
                {
                    throw new Exception($"找不到工號{vm.EId}員工資訊");
                }
                string fileName = $"{DateTime.Now.Ticks}{vm.ImageFile.FileName}";
                emp.Name = vm.Name;
                emp.EnglishName = vm.EnglishName;
                emp.Email = vm.Email;
                emp.Phone = vm.Phone;
                emp.Image = fileName;
                await db.SaveChangesAsync();
                result.isPass = true;
                result.msg = "資料修改成功!";
                result.FileName = fileName;
                return result;
            }
        }
    }
}