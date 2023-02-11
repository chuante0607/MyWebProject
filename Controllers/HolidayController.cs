using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Interop;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HolidayController : Controller
    {
        public ActionResult Apply()
        {
            HolidayViewModel vmHoliday = new HolidayViewModel();
            vmHoliday.employee = SessionEmp.CurrentEmp;
            vmHoliday.Holidays = HolidayUtility.getCanUseHolidaysByEmpID(vmHoliday.employee.EId);
            vmHoliday.WorkDayOfYearByMonth = HolidayUtility.getWorkDayOfYearByMonth(DateTime.Now.Year);
            ViewBag.vm = JsonConvert.SerializeObject(vmHoliday, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View(vmHoliday);
        }

        [HttpPost]
        public async Task<ActionResult> Apply(HolidayDetailModel payload)
        {
            //to do 驗證休假申請

            bool error = true;
            string msg = string.Empty;

            //模型驗證
            if (ModelState.Values.Where(w => w.Errors.Count > 0).Count() > 0)
            {
                msg = "表單填寫有誤\r\n請重新確認";
                return Json(new { error = error, msg = msg, payload = payload });
            }

            //模型驗證
            if (ModelState.IsValid)
            {
                //是否申請成功(true成功 , false失敗)
                var isApplyOK = await HolidayUtility.validApplyResult(payload);
                error = !isApplyOK.Item1;
                msg = isApplyOK.Item2;
            }
            else
            {
                msg = "表單驗證異常，請重新確認";
            }
            return Json(new { error = error, msg = msg, payload = payload });
        }
    }
}