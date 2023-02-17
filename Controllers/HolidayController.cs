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
using UCOMProject.Extension;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HolidayController : Controller
    {
        HolidayDetailViewModel vm = new HolidayDetailViewModel();
        public async Task<ActionResult> Apply()
        {
            vm.Employee = SessionEmp.CurrentEmp;
            vm.Holidays = await HolidayUtility.GetHolidayInfos(vm.Employee.EId);
            vm.WorkDayOfYearByMonth = HolidayUtility.getWorkDayOfYearByMonth(DateTime.Now.Year);
            ViewBag.vm = JsonConvert.SerializeObject(vm, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View(vm);
        }

        [HttpPost]
        public ActionResult Apply(HolidayDetailViewModel payload)
        {
            //to do 驗證休假申請
            //模型驗證
            if (ModelState.IsValid)
            {
                //病假以外不需提供證明
                if (payload.Title.xTranEnum() != HolidayType.病假)
                {
                    //是否申請成功(true成功 , false失敗)
                    ApplyViewModel result = HolidayUtility.saveApply(payload);
                    return Json(result);
                }

                //病假有無提供證明
                if (payload.Files != null)
                {
                    double size = payload.Files.Select(s => s.ContentLength).Sum() / (1024d * 1024d);
                    if (size > 4)
                        return Json(new ApplyViewModel { Error = true, Msg = "檔案大小不能超過4MB" });
                    //是否申請成功(true成功 , false失敗)
                    ApplyViewModel result = HolidayUtility.saveApplyWithFiles(payload);
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
                    return Json(new ApplyViewModel { Error = true, Msg = $"{payload.Title}需要提供證明" });
                }
            }
            else
            {
                return Json(new ApplyViewModel { Error = true, Msg = "表單驗證異常\r\n請重新確認" });
            }
        }


        public ActionResult Index(string eid)
        {
            //vm.employee = SessionEmp.CurrentEmp;
            //vm.HolidayDetails = HolidayUtility.getHolidayDetailList(eid);
            return View();
        }
    }
}