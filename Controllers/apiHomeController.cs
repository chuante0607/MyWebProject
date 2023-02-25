using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UCOMProject.Methods;
using UCOMProject.Models;

namespace UCOMProject.Controllers
{
    [AuthorizationFilter]
    public class apiHomeController : ApiController
    {

        // GET: api/apiHome
        public JsonResult Get()
        {
          
            return null;
        }

        // GET: api/apiHome/5
        public SummaryViewModel Get(string Eid)
        {
          
            return null;
        }

        // POST: api/apiHome
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/apiHome/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/apiHome/5
        public void Delete(int id)
        {
        }
    }
}
