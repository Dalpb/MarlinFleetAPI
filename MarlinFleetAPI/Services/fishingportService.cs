using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Services
{
    public class fishingportService
    {
        private MarlinFleetBDEntities marlinBD;

        public fishingportService()
        {
            marlinBD = new MarlinFleetBDEntities();
        }

        public List<tbl_fishingport> ListAllPorts()
        {
            return marlinBD.tbl_fishingport.ToList();
        }

        public tbl_fishingport findPort(Guid id)
        {
            return marlinBD.tbl_fishingport.Find(id);
        }

        public tbl_fishingport CreateNewPort( tbl_fishingport newPort)
        {
            tbl_fishingport portEntity = null;
            portEntity = marlinBD.tbl_fishingport.Add(newPort);
            marlinBD.SaveChanges();
            return portEntity;
        }

        public void UpdatePort(tbl_fishingport currentPort,tbl_fishingport newPort)
        {
            currentPort.name = newPort.name;
            currentPort.location = newPort.location;
            currentPort.capacity = newPort.capacity;
            currentPort.tbl_boat = newPort.tbl_boat;
            marlinBD.SaveChanges();
        }

    }
}