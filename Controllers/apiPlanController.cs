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
using UCOMProject.API;
using UCOMProject.Methods;
using UCOMProject.Models;
using static UCOMProject.Controllers.apiScheduleController;

namespace UCOMProject.Controllers
{
    public class apiPlanController : ApiController
    {
        JsonSerializerSettings camelSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        /// <summary>
        /// 查詢Plan schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            ScheduleApiModel schedule = await ScheduleUtility.GetSchedule();
            return Json(JsonConvert.SerializeObject(schedule, camelSetting));
        }


        /// <summary>
        /// 新增或修改Plan事件
        /// </summary>
        /// <param name="plans"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApplyResult> Post([FromBody] List<Plan> plans)
        {
            ApplyResult result = await ScheduleUtility.HandlePlans(plans);
            return result;
        }

        /// <summary>
        /// 刪除Plan事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApplyResult> Delete(Guid id)
        {
            return await ScheduleUtility.DeletePlan(id);
        }
    }
}
