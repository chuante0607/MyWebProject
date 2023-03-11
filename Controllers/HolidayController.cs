using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UCOMProject.Interfaces;
using UCOMProject.Methods;
using UCOMProject.Models;
using UCOMProject.Roles;
using System.Text;
using System.Globalization;
using UCOMProject.Extension;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class HolidayController : Controller
    {
        HolidayDetailTableViewModel vmTable = new HolidayDetailTableViewModel();
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };




        [HttpGet]
        /// <summary>
        /// 休假申請
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Apply(string eid, string shift)
        {
            ApplyViewModel vm = new ApplyViewModel();
            RoleManage user = ConfirmIdentity();

            vm.Employee = await user.GetUser();
            vm.Holidays = await user.GetHolidayInfosByEmp(eid);
            if (shift.xTranShiftEnum() == ShiftType.常日班)
            {
                string[] file = System.IO.File.ReadAllLines(Server.MapPath("~/Uploads/112年中華民國政府行政機關辦公日曆表.csv"), Encoding.Default);
                vm.WorkDayOfYearByMonth = user.GetWorkDayOfYearByMonth(file, DateTime.Now.Year);
            }
            else
            {
                vm.WorkDayOfYearByMonth = user.GetWorkDayOfYearByMonth(shift.xTranShiftEnum(), DateTime.Now.Year);
            }
            ViewBag.vm = JsonConvert.SerializeObject(vm, camelSetting);
            return View(vm);
        }

        /// <summary>
        /// 休假申請POST
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
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
                        return Json(new ApplyResult { isPass = false, msg = $"{payload.Title}需要提供證明" });
                    //附加檔案超過4MB
                    double size = payload.Files.Select(s => s.ContentLength).Sum() / (1024d * 1024d);
                    if (size > 4)
                        return Json(new ApplyResult { isPass = false, msg = "檔案大小不能超過4MB" });
                    //是否申請成功(true成功 , false失敗)
                    ApplyResult result = HolidayUtility.SaveApply(payload);
                    if (result.isPass)
                    {
                        //將檔案儲存
                        foreach (var file in payload.Files.Select((value, index) => new { value, index }))
                        {
                            string path = Server.MapPath($@"\Uploads\{result.FilesNames[file.index]}");
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
                return Json(new ApplyResult { isPass = false, msg = "表單驗證異常\r\n請重新確認" });
            }
        }

        /// <summary>
        /// 查詢員工自己休假送出狀態(待審核以及退回狀態)
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            RoleManage user = new User(RoleType.User);
            vmTable.Employee = await user.GetUser();
            vmTable.Details = await user.GetHolidayDetails();
            ViewBag.Source = JsonConvert.SerializeObject(vmTable, camelSetting);
            return View(vmTable);
        }

        /// <summary>
        /// 員工刪除休假申請
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Delete(List<int> id)
        {
            RoleManage user = new User(RoleType.User);
            ApplyResult result = new ApplyResult();
            result.isPass = await user.DelHolidayDetails(id);
            if (result.isPass)
            {
                result.msg = "刪除成功";
            }
            else
            {
                result.msg = "刪除失敗!";
            }
            return Json(result);
        }

        /// <summary>
        /// 編輯休假資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Record(string id)
        {
            RoleManage user = ConfirmIdentity();
            List<HolidayDetailViewModel> holidays = new List<HolidayDetailViewModel>();
            if (id != null && user.GetRole() != RoleType.User)
            {
                bool isId = int.TryParse(id, out int detailId);
                if (isId)
                    holidays.Add(await HolidayUtility.GetHolidayDetail(detailId));
            }
            else
            {
                holidays = await user.GetHolidayDetails();
            }
            List<HolidayDetailViewModel> holidayDetails = holidays.Where(w => w.State == (int)ReviewType.Pass).ToList();
            ViewBag.Source = JsonConvert.SerializeObject(holidayDetails, camelSetting);
            return View();
        }

        //-----------------------主管權限才可以訪問的頁面-------------------------------

        /// <summary>
        /// 主管審核休假
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Review(int? rank)
        {
            RoleManage user = null;
            if (rank == null)
            {
                user = ConfirmIdentity();
                if (user.Role == RoleType.User)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                switch ((int)rank)
                {
                    case 0:
                        user = new Admin(RoleType.Admin);
                        break;
                    case 2:
                        user = new Manager(RoleType.Manager, SessionEmp.CurrentEmp.Branch.xTranBranchEnum());
                        break;
                    default:
                        break;
                }
            }
            vmTable.Employee = await user.GetUser();
            var query = await user.GetHolidayDetails();
            vmTable.Details = query.Where(w => w.State == (int)ReviewType.Wait).ToList();
            ViewBag.Source = JsonConvert.SerializeObject(vmTable, camelSetting);
            return View(vmTable);
        }

        /// <summary>
        /// 變更休假申請狀態
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Review(List<HolidayDetailViewModel> payload)
        {
            string act = payload.Select(s => s.Action).FirstOrDefault();
            //確認目前使用者權限
            RoleManage user = ConfirmIdentity();
            //管理職與admin才可以審核員工休假申請
            IHolidayReviewer allower = null;
            ApplyResult result = new ApplyResult();
            result.isPass = false;
            switch (user.Role)
            {
                case RoleType.Admin:
                    allower = new Admin(RoleType.Admin);
                    break;
                case RoleType.Manager:
                    allower = new Manager(RoleType.Manager);
                    break;
                default:
                    result.msg = "目前權限無法審核！";
                    return Json(result);
            }
            switch (act)
            {
                case "pass":
                    result.isPass = await allower.ReviewHolidayApply(payload, ReviewType.Pass);
                    break;
                case "back":
                    result.isPass = await allower.ReviewHolidayApply(payload, ReviewType.Back);
                    break;
                default:
                    break;
            }
            if (result.isPass)
                result.msg = "審核完成！";
            else
                result.msg = "審核異常！";
            return Json(result);

        }

        /// <summary>
        /// 取得目前使用者身分
        /// </summary>
        /// <returns></returns>
        private RoleManage ConfirmIdentity()
        {
            RoleManage user = null;
            switch (SessionEmp.CurrentEmp.JobRank)
            {
                case 0:
                    user = new Admin(RoleType.Admin);
                    break;
                case 1:
                    user = new User(RoleType.User);
                    break;
                case 2:
                    user = new Manager(RoleType.Manager, SessionEmp.CurrentEmp.Branch.xTranBranchEnum());
                    break;
            }
            return user;
        }
    }
}