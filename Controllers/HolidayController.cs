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
        public async Task<ActionResult> Apply()
        {
            ApplyViewModel vm = new ApplyViewModel();
            vm.Employee = SessionEmp.CurrentEmp;
            vm.Holidays = await HolidayUtility.GetHolidayInfos(vm.Employee.EId);
            vm.WorkDayOfYearByMonth = HolidayUtility.GetWorkDayOfYearByMonth(DateTime.Now.Year);
            ViewBag.vm = JsonConvert.SerializeObject(vm, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View(vm);
        }

        [HttpPost]
        public ActionResult Apply(ApplyViewModel payload)
        {
            //to do 驗證休假申請
            //模型驗證
            if (ModelState.IsValid)
            {
                //需要證明
                if (payload.ProveType)
                {
                    //沒附加檔案
                    if (payload.Files == null)
                        return Json(new ApplyResult { Error = true, Msg = $"{payload.Title}需要提供證明" });
                    //附加檔案超過4MB
                    double size = payload.Files.Select(s => s.ContentLength).Sum() / (1024d * 1024d);
                    if (size > 4)
                        return Json(new ApplyResult { Error = true, Msg = "檔案大小不能超過4MB" });
                    //是否申請成功(true成功 , false失敗)
                    ApplyResult result = HolidayUtility.SaveApply(payload);
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
                //不需要證明
                ApplyResult result1 = HolidayUtility.SaveApply(payload);
                return Json(result1);
            }
            else
            {
                //表單異常
                return Json(new ApplyResult { Error = true, Msg = "表單驗證異常\r\n請重新確認" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index(string eid)
        {
            HolidayDetailTableViewModel table = new HolidayDetailTableViewModel();
            table.Employee = await EmployeeUtility.GetEmp(eid);
            table.Details = await HolidayUtility.GetHolidayDetailList(eid);
            if (SessionEmp.CurrentEmp.JobRank == 1)
            {
                table.OtherEmps = await EmployeeUtility.GetEmpsByShift(SessionEmp.CurrentEmp.Shift.xShiftTranEnum());
            }
            ViewBag.Source = JsonConvert.SerializeObject(table, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return View(table);
        }

        [HttpPost]
        public ActionResult Delete(List<int> id, string eid)
        {
            foreach (int item in id)
            {
                HolidayUtility.DelHolidayDetail(item, eid);
            }
            return View();
        }
    }
}