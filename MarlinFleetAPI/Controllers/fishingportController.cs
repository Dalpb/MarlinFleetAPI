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
using log4net;
using System.Runtime.Remoting.Messaging;

namespace MarlinFleetAPI.Controllers
{
    [RoutePrefix("fishingport")]
    public class fishingportController : ApiController
    {
        private fishingportService FishingportService = new fishingportService();
        private static readonly ILog logger = LogManager.GetLogger(typeof(fishingportController));

        [HttpGet]
        public IHttpActionResult GetAllPorts()
        {
            var response = new ApiResponse<List<tbl_fishingport>>("", null, false);
            logger.Info("Fetching GET all Fishing Ports");
            List<tbl_fishingport> listports = new List<tbl_fishingport>();
            try
            {
                listports = FishingportService.ListAllPorts();
                response.message = "Fetching Port's list success";
                response.success = true;
                response.data = listports;
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error($"ERROR : {ex.Message}");
                response.message = "Fetching ports error";
                return Content(HttpStatusCode.BadRequest, response);
            }
        }
        [HttpGet]
        public IHttpActionResult GetPortById(Guid uuid)
        {
            var response = new ApiResponse<tbl_fishingport>("",null,false);
            var port = FishingportService.FindPort(uuid);
            if(port is null)
            { 
                response.message = "Not Found port: " + uuid;
                return Content(HttpStatusCode.NotFound, response);
            }
            response.success = true;
            response.message = "Port Founded: " + uuid;
            response.data = port;
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
        public IHttpActionResult PutFishingPort(Guid uuid, [FromBody] tbl_fishingport upfishingport)
        {
            var response = new ApiResponse<tbl_fishingport>("", null, false);
            if(upfishingport.id != uuid)
            {
                response.message = "Id is not equal";
                return Content(HttpStatusCode.BadRequest, response);
            }
            var currPort = FishingportService.FindPort(uuid);
            if (currPort is null)
            {
                response.message = "Not Found port'id: " + uuid;
                return Content(HttpStatusCode.NotFound, response);
            }
            try
            {
                FishingportService.UpdatePort(currPort, upfishingport);
                response.message = "Port update successfuly";
                response.success = true;
                response.data = currPort;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Server Error";
                return Content(HttpStatusCode.InternalServerError, response); 
            }
        }

        [HttpPatch]
        public IHttpActionResult PatchFishingPort(Guid uuid, [FromBody] dynamic partBody)
        {
            var response = new ApiResponse<tbl_fishingport>("", null, false);
            var currPort = FishingportService.FindPort(uuid);
            if(currPort is null)
            {
                response.message = "Not Found port'id: " + uuid;
                return Content(HttpStatusCode.NotFound, response);
            }
            try
            {
                FishingportService.PatchPartialPort(currPort, partBody);
                response.message = "Port update successfuly";
                response.success = true;
                response.data = currPort;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Server Error";
                return Content(HttpStatusCode.InternalServerError, response);
            }

        }

        [HttpDelete]
        public IHttpActionResult DeleteFishingPort(Guid uuid) { 
            var response = new ApiResponse<tbl_fishingport>("", null, false);
            var port = FishingportService.FindPort(uuid);
            if(port is null)
            {
                response.message = "Not Found port'id: " + uuid;
                return Content(HttpStatusCode.NotFound, response);
            }
            try
            {
                FishingportService.DeletePort(port);
                response.success = true;
                return Content(HttpStatusCode.NoContent, response);
            }
            catch(Exception e)
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