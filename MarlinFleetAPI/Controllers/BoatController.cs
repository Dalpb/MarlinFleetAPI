using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using MarlinFleetAPI.Models;
using MarlinFleetAPI.Repository;
using MarlinFleetAPI.Services;

namespace MarlinFleetAPI.Controllers
{
    [RoutePrefix("boat")]
    public class BoatController : ApiController
    {
        private BoatService boatService;
        private static ILog Logger = LogManager.GetLogger(typeof(BoatController));

        [HttpGet]
        public async Task<IHttpActionResult> GetAllBoats()
        {
            Logger.Info("GET: /boat");
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    boatService = new BoatService(unitOfWork);
                    List<tbl_boat> boats = await boatService.ListAllBoats();
                    return Ok(boats);
                }
                catch (Exception ex) {
                    Logger.Error("Error " + ex);
                    return InternalServerError(ex);
                }
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBoatByUUID(Guid uuid)
        {
            Logger.Info($"GET: /boat: {uuid}");
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    boatService = new BoatService(unitOfWork);
                    tbl_boat boat = await boatService.FindBoatByUUID(uuid);
                    return Ok(boat);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception ex) { 
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostNewBoat([FromBody] tbl_boat boat)
        {
            Logger.Info("POST: /boat");
            using(var unitOfWork = new UnitOfWork())
            {
                try
                {
                    boatService = new BoatService(unitOfWork);
                    tbl_boat nboat = await boatService.RegisterBoat(boat);
                    return Created("/boat", nboat);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (InvalidOperationException ex) { 
                    return BadRequest(ex.Message);
                }
                catch(Exception ex) {
                    return InternalServerError(ex);
                }
            }

        }

    }
}
