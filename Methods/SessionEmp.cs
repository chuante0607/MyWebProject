using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCOMProject.Models;

namespace UCOMProject.Methods
{
    public class SessionEmp
    {
        public static Employee CurrentEmp
        {
            get
            {
                return HttpContext.Current.Session["emp"] as Employee;
            }
        }
    }
}