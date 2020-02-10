using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace TokenBasedAuthenticaiton.Controllers
{
    public class DataController : ApiController
    {
        
        [Route("api/data/get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("It has not need authenticaiton");
        }

        [Authorize]
        [Route("api/data/User")]
        [HttpGet]
        public IHttpActionResult GetForUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("hello "+identity.Name);
        }

        [Authorize(Roles ="admin")]
        [Route("api/data/Admin")]
        [HttpGet]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var role = identity.Claims
                .Where(r => r.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return Ok("hello "+identity.Name+"Role : " + string.Join(",",role.ToList()));
        }
    }
}
