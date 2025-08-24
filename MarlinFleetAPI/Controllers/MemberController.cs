using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MarlinFleetAPI.Controllers
{
    [RoutePrefix("member")]
    public class MemberController : ApiController
    {
        public MemberController() {

        }

        [HttpGet, Route("")]
        public IHttpActionResult GetAllMember()
        {

        }
    }
}
