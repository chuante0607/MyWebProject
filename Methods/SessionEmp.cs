using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class SessionEmp
    {
        public static EmployeeModel CurrentEmp
        {
            get
            {
                return HttpContext.Current.Session["emp"] as EmployeeModel;
            }
        }
    }
}