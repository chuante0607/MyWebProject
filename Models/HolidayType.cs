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
        事假,
        病假,
        生理假,
        家庭照顧假,
        產檢假,
        陪產假,
        安胎假,
    }
}