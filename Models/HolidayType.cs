using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UCOMProject.Models
{
    
    public enum HolidayType
    {
        特休 = 1,
        事假 = 2,
        病假 = 3,
        生理假 = 4,
        家庭照顧假 = 5,
        產檢假 = 6,
        陪產假 = 7,
        安胎假 = 8,
    }
}