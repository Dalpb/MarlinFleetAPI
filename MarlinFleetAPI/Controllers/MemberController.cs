using MarlinFleetAPI.Models;
using MarlinFleetAPI.Repository;
using MarlinFleetAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MarlinFleetAPI.Controllers
{
    [RoutePrefix("member")]
    public class MemberController : ApiController
    {
        private MemberService memberService ;

        [HttpGet]
        public async Task<IHttpActionResult> GetAllMembers()
        {
            using (var unitOfWork = new UnitOfWork()) // para que se ejecute el Dispose, libera conexion
            {
                try
                {
                    memberService = new MemberService(unitOfWork);
                    var members = await memberService.ListAllMembers();
                    return Ok(members);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        [HttpGet]
        public IHttpActionResult GetMember(Guid uuid)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    memberService = new MemberService(unitOfWork);
                    var member = memberService.FindMemberById(uuid);
                    if (member is null)
                        return NotFound();

                    else return Ok(member);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult PostMember([FromBody] tbl_member memberBody)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    memberService = new MemberService(unitOfWork);

                    tbl_member member = memberService.RegisterMember(memberBody);
                    return Content(HttpStatusCode.Created, member);
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }   
            }
        }

        [HttpPut]
        public IHttpActionResult PutMember(Guid uuid,[FromBody] tbl_member upmember)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    memberService = new MemberService(unitOfWork);
                     _ = memberService.UpdateMember(uuid, upmember);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (KeyNotFoundException) 
                {
                    return NotFound();
                }
                catch (InvalidOperationException ex) {
                    return BadRequest(ex.Message);
                }
                catch(Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteMember(Guid uuid)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    memberService = new MemberService(unitOfWork);
                    memberService.DeleteMember(uuid);
                    return StatusCode(HttpStatusCode.NoContent);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch(Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        }

    }
}
