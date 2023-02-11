using System;
using System.Linq;
using System.Reflection;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class EmployeeUtility
    {
        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static EmployeeModel getEmp(string id, string pwd)
        {
            EmployeeModel employee = new EmployeeModel();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.SingleOrDefault(e => e.EId == id && e.Password == pwd);
                //員工基本資料
                //將DB的employee轉換至employeeModel
                if (emp == null)
                    return null;
                Type typeA = emp.GetType();
                Type typeB = employee.GetType();
                PropertyInfo[] props = typeA.GetProperties();
                foreach (PropertyInfo proA in props)
                {
                    PropertyInfo proB = typeB.GetProperty(proA.Name);
                    if (proB != null)
                    {
                        proB.SetValue(employee, proA.GetValue(emp));
                    }
                }
            }
            return employee;
        }

        public static EmployeeModel getEmp(string id)
        {
            EmployeeModel employee = new EmployeeModel();
            using (MyDBEntities db = new MyDBEntities())
            {
                Employee emp = db.Employees.SingleOrDefault(e => e.EId == id);
                if (emp == null)
                    return null;
                //員工基本資料
                //將DB的employee轉換至employeeModel
                Type typeA = emp.GetType();
                Type typeB = employee.GetType();
                PropertyInfo[] props = typeA.GetProperties();
                foreach (PropertyInfo proA in props)
                {
                    PropertyInfo proB = typeB.GetProperty(proA.Name);
                    if (proB != null)
                    {
                        proB.SetValue(employee, proA.GetValue(emp));
                    }
                }
            }
            return employee;
        }
    }
}