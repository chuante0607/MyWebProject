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
                if (HttpContext.Current.Session == null)
                    return null;
                if (HttpContext.Current.Session["emp"] == null)
                    return null;
                return HttpContext.Current.Session["emp"] as Employee;
            }
        }
    }
}