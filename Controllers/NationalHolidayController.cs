using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace UCOMProject.Controllers
{
    public class NationalHolidayController : ApiController
    {
        //public class NationalHoliday
        //{
        //    public NationalHoliday(DateTime date, bool isHoliday, string remark)
        //    {
        //        this.Date = date;
        //        this.IsHoliday = isHoliday;
        //        this.Remark = remark;
        //    }
        //    public DateTime Date { get; set; }
        //    public bool IsHoliday { get; set; }
        //    public string Remark { get; set; }
        //}

        public JsonSerializerSettings CamelSetting = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        // GET: api/NationalHoliday
        public IHttpActionResult Get()
        {
            //var file = File.ReadAllLines(HttpContext.Current.Server
            //    .MapPath("~/Uploads/112年中華民國政府行政機關辦公日曆表.csv"), Encoding.Default);
            //List<NationalHoliday> nationalHolidays = new List<NationalHoliday>();
            //foreach (var line in file)
            //{
            //    List<string> str = line.Split(',').ToList();
            //    if (DateTime.TryParseExact(str[0], "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            //    {
            //        if (str[2] == "2")
            //            nationalHolidays.Add(new NationalHoliday(date, true, str[3]));
            //        else
            //            nationalHolidays.Add(new NationalHoliday(date, false, str[3]));
            //    }
            //}
            //return Json(nationalHolidays, CamelSetting);
            return Ok();
        }

        // GET: api/NationalHoliday/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NationalHoliday
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NationalHoliday/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NationalHoliday/5
        public void Delete(int id)
        {
        }
    }
}
