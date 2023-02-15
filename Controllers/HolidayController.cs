using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Interop;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HolidayController : Controller
    {
        HolidayViewModel vmHoliday = new HolidayViewModel();
        public ActionResult Apply()
        {
            vmHoliday.employee = SessionEmp.CurrentEmp;
            vmHoliday.Holidays = HolidayUtility.getCanUseHolidaysByEmpID(vmHoliday.employee.EId);
            vmHoliday.WorkDayOfYearByMonth = HolidayUtility.getWorkDayOfYearByMonth(DateTime.Now.Year);
            ViewBag.vm = JsonConvert.SerializeObject(vmHoliday, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View(vmHoliday);
        }

        [HttpPost]
        public ActionResult Apply(HolidayDetailModel payload)
        {
            //to do 驗證休假申請
            //模型驗證
            if (ModelState.IsValid)
            {
                //病假以外不需提供證明
                if (payload.Title != HolidayType.病假.ToString())
                {
                    //是否申請成功(true成功 , false失敗)
                    Apply result = HolidayUtility.saveApply(payload);
                    return Json(result);
                }

                //病假有無提供證明
                if (payload.Files != null)
                {
                    double size = payload.Files.Select(s => s.ContentLength).Sum() / (1024d * 1024d);
                    if (size > 4)
                        return Json(new Apply { Error = true, Msg = "檔案大小不能超過4MB" });
                    //是否申請成功(true成功 , false失敗)
                    Apply result = HolidayUtility.saveApplyWithFiles(payload);
                    if (!result.Error)
                    {
                        //將檔案儲存
                        foreach (var file in payload.Files.Select((value, index) => new { value, index }))
                        {
                            string path = Server.MapPath($@"\Uploads\{result.FilesName[file.index]}");
                            file.value.SaveAs(path);
                        }
                    }
                    return Json(result);
                }
                else
                {
                    return Json(new Apply { Error = true, Msg = $"{payload.Title}需要提供證明" });
                }
            }
            else
            {
                return Json(new Apply { Error = true, Msg = "表單驗證異常\r\n請重新確認" });
            }
        }

        public ActionResult Index(string eid)
        {

            return View();
        }
    }
}