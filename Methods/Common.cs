using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UCOMProject.Methods
{
    public class Common
    {
        public static string cnStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            }
        }
    }
}