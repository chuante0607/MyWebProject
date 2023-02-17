using System;
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
                //將DB的employee轉換至employeeModel
                //if (emp == null)
                //    return null;
                //Type typeA = emp.GetType();
                //Type typeB = employee.GetType();
                //PropertyInfo[] props = typeA.GetProperties();
                //foreach (PropertyInfo proA in props)
                //{
                //    PropertyInfo proB = typeB.GetProperty(proA.Name);
                //    if (proB != null)
                //    {
                //        proB.SetValue(employee, proA.GetValue(emp));
                //    }
                //}
                return await db.Employees.SingleOrDefaultAsync(e => e.EId == id && e.Password == pwd);
            }
        }
    }
}