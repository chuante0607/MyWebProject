using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCOMProject.Methods;
using UCOMProject.Models;
using static UCOMProject.Controllers.apiScheduleController;

namespace UCOMProject.Controllers
{
    public class apiPlanController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };


        //public class PlanViewModel
        //{
        //    public Guid Id { get; set; }
        //    public string planTitle { get; set; }
        //    public DateTime startDate { get; set; }
        //    public DateTime endDate { get; set; }
        //}

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            ScheduleViewModel schedule = new ScheduleViewModel();
            using (MyDBEntities db = new MyDBEntities())
            {
                schedule.Shifts = ScheduleUtility.GetWorkDayOfYearByMonth(ShiftType.A班, DateTime.Now.Year);
                schedule.plans = await db.Plans.ToListAsync();
                return Json(JsonConvert.SerializeObject(schedule, camelSetting));
            }
        }

        /// <summary>
        /// plan產生Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Guid Get(string id)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 新增或修改Plan事件
        /// </summary>
        /// <param name="plans"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] List<Plan> plans)
        {
            try
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    List<Plan> data = await db.Plans.ToListAsync();
                    foreach (Plan p in plans)
                    {
                        var hasData = data.FirstOrDefault(f => f.Id == p.Id);
                        if (hasData == null)
                        {
                            db.Plans.Add(new Plan
                            {
                                Id = p.Id,
                                StartDate = p.StartDate,
                                EndDate = p.EndDate,
                                PlanTitle = p.PlanTitle
                            });
                        }
                        else
                        {
                            hasData.StartDate = p.StartDate;
                            hasData.EndDate = p.EndDate;
                            hasData.PlanTitle = p.PlanTitle;
                        }
                    }

                    await db.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        // PUT: api/apiPlan/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/apiPlan/5
        public void Delete(Guid id)
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                var plan = db.Plans.Find(id);
                if (plan != null)
                {
                    db.Plans.Remove(plan);
                    db.SaveChanges();
                }
            }
        }
    }
}
