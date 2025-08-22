using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using MarlinFleetAPI.Models;
using MarlinFleetAPI.Services;

namespace MarlinFleetAPI.Controllers
{
    [RoutePrefix("/fishingport")]
    public class fishingportController : ApiController
    {
        private fishingportService FishingportService = new fishingportService();

        [HttpGet]
        public IHttpActionResult GetAllPorts()
        {
            List<tbl_fishingport> listports = new List<tbl_fishingport>();
            listports = FishingportService.ListAllPorts();
            var response = new ApiResponse<List<tbl_fishingport>>("port's list success",listports,true);
            return Ok(response);
        }
        [HttpGet]
        public IHttpActionResult GetPortById(Guid uuid)
        {
            var port = FishingportService.findPort(uuid);
            var response = new ApiResponse<tbl_fishingport>("",null,false);
            if(port is null)
            { 
                response.message = "Not Found port: " + uuid;
                return Content(HttpStatusCode.NotFound, response);
            }
            response.success = true;
            response.message = "Port Founded: " + uuid;
            return Ok(response);
        }

        [HttpPost]
        public IHttpActionResult PostNewPort([FromBody] tbl_fishingport fishingport)
        {
            try
            {
                tbl_fishingport port = FishingportService.CreateNewPort(fishingport);
                var response = new ApiResponse<tbl_fishingport>("port created",port,true);
                return Created("/fishinport", response);
            }catch(DbUpdateException ex)
            {
                var response = new ApiResponse<object>("Error create", null, false);
                return Content(HttpStatusCode.Conflict, response);
            }
            catch(Exception ex)
            {
                var response = ApiResponse<object>.SystemErrorResponse();
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut]
        public IHttpActionResult PutFishingPort(Guid id, [FromBody] tbl_fishingport upfishingport)
        {
            var response = new ApiResponse<tbl_fishingport>("", null, false);
            if(upfishingport.id == id)
            {
                response.message = "Id is not equal";
                return Content(HttpStatusCode.BadRequest, response);
            }
            var currPort = FishingportService.findPort(id);
            if (currPort is null)
            {
                response.message = "Not Found port'id: " + id;
                return Content(HttpStatusCode.NotFound, response);
            }
            try
            {
                FishingportService.UpdatePort(currPort, upfishingport);
                response.message = "Port update successfuly";
                response.success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Server Error";
                return Content(HttpStatusCode.InternalServerError, response); 
            }
        }


        // GET: api/fishingport
        /*   public IQueryable<tbl_fishingport> Gettbl_fishingport()
           {
               return db.tbl_fishingport;
           }

           // GET: api/fishingport/5
           [ResponseType(typeof(tbl_fishingport))]
           public IHttpActionResult Gettbl_fishingport(Guid id)
           {
               tbl_fishingport tbl_fishingport = db.tbl_fishingport.Find(id);
               if (tbl_fishingport == null)
               {
                   return NotFound();
               }

               return Ok(tbl_fishingport);
           }

           // PUT: api/fishingport/5
           [ResponseType(typeof(void))]
           public IHttpActionResult Puttbl_fishingport(Guid id, tbl_fishingport tbl_fishingport)
           {
               if (!ModelState.IsValid)
               {
                   return BadRequest(ModelState);
               }

               if (id != tbl_fishingport.id)
               {
                   return BadRequest();
               }

               db.Entry(tbl_fishingport).State = EntityState.Modified;

               try
               {
                   db.SaveChanges();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!tbl_fishingportExists(id))
                   {
                       return NotFound();
                   }
                   else
                   {
                       throw;
                   }
               }

               return StatusCode(HttpStatusCode.NoContent);
           }

           // POST: api/fishingport
           [ResponseType(typeof(tbl_fishingport))]
           public IHttpActionResult Posttbl_fishingport(tbl_fishingport tbl_fishingport)
           {
               if (!ModelState.IsValid)
               {
                   return BadRequest(ModelState);
               }

               db.tbl_fishingport.Add(tbl_fishingport);

               try
               {
                   db.SaveChanges();
               }
               catch (DbUpdateException)
               {
                   if (tbl_fishingportExists(tbl_fishingport.id))
                   {
                       return Conflict();
                   }
                   else
                   {
                       throw;
                   }
               }

               return CreatedAtRoute("DefaultApi", new { id = tbl_fishingport.id }, tbl_fishingport);
           }

           // DELETE: api/fishingport/5
           [ResponseType(typeof(tbl_fishingport))]
           public IHttpActionResult Deletetbl_fishingport(Guid id)
           {
               tbl_fishingport tbl_fishingport = db.tbl_fishingport.Find(id);
               if (tbl_fishingport == null)
               {
                   return NotFound();
               }

               db.tbl_fishingport.Remove(tbl_fishingport);
               db.SaveChanges();

               return Ok(tbl_fishingport);
           }

           protected override void Dispose(bool disposing)
           {
               if (disposing)
               {
                   db.Dispose();
               }
               base.Dispose(disposing);
           }

           private bool tbl_fishingportExists(Guid id)
           {
               return db.tbl_fishingport.Count(e => e.id == id) > 0;
           }*/
    }
}