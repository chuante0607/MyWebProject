using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

            if (ModelState.Values.Where(w => w.Errors.Count > 0).Count() > 0)
                return Json(new { error = true, msg = "表單填寫有誤\r\n請重新確認", payload = payload });

            if (ModelState.IsValid)
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    var query = await db.HolidayDetails.Where(w => w.EId == payload.EId).OrderBy(o => o.BeginDate).ToListAsync();

                    //取得payload休假的月份
                    List<int> getMonths = payload.RangeDate.Select(s => ((DateTime)s).Month).OrderBy(o => o).Distinct().ToList();

                    foreach (var item in query)
                    {
                        //判斷該員工休假detail之中有沒有與申請休假的月份相同 ,Any查到即返回true
                        if (getMonths.Any(o => o == item.BeginDate.Month || o == item.EndDate.Month))
                        {
                            //usedDays計算該員工detail的結束日期-開始日期的總天數
                            int checkDay = item.EndDate.Subtract(item.BeginDate).Days;
                            while (checkDay >= 0)
                            {
                                //請假範圍 >= 0天(包括當天) 就從開始的日期+天數那天開始判斷
                                DateTime checkDate = item.EndDate.AddDays(checkDay * -1);

                                //payload送過來的RangeDate含有員工選擇休假期間的所有日期集合,拿來判斷是否跟資料庫的日期是否重複
                                var result = payload.RangeDate
                                       .Where(date =>
                                       (new DateTime(((DateTime)date).Year, ((DateTime)date).Month, ((DateTime)date).Day)).Ticks
                                       .Equals(new DateTime(checkDate.Year, checkDate.Month, checkDate.Day).Ticks))
                                       .FirstOrDefault();
                                if (result != null)
                                {
                                    string msg = $"{((DateTime)result).Date.ToShortDateString()}\r\n已有休假紀錄，請重新申請";
                                    return Json(new { error = true, msg = msg, payload = payload });
                                }
                                checkDay--;
                            }
                        }
                    }
                    //判斷結束寫進資料庫
                    HolidayDetail holidayDetail = new HolidayDetail()
                    {
                        EId = payload.EId,
                        HId = (int)(payload.HId),
                        BeginDate = (DateTime)payload.BeginDate,
                        EndDate = (DateTime)payload.EndDate,
                        UsedDays = (double)payload.UsedDays,
                        Allow = false,
                        Remark = payload.Remark,
                    };
                    db.HolidayDetails.Add(holidayDetail);
                    await db.SaveChangesAsync();
                }
            }
            return Json(new
            {
                error = false,
                msg = $"申請已送出！\r\n" +
                $"{((DateTime)payload.BeginDate).ToShortDateString()} ~ " +
                $"{((DateTime)payload.EndDate).ToShortDateString()}\r\n共計：{payload.Title}{payload.UsedDays}天"
            }
            );
        }
    }
}