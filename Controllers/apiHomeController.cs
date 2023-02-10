using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        HomeViewModel vm = new HomeViewModel();

        // GET: api/apiHome
        public HomeViewModel Get()
        {
            return null;
        }

        // GET: api/apiHome/5
        public HomeViewModel Get(string Eid)
        {
          
            return vm;
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
