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

        public tbl_fishingport FindPort(Guid id)
        {
            return marlinBD.tbl_fishingport.Find(id);
        }

        public tbl_fishingport CreateNewPort( tbl_fishingport newPort)
        {
            tbl_fishingport portEntity = marlinBD.tbl_fishingport.Add(newPort);
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

        public void PatchPartialPort(tbl_fishingport currentPort, dynamic partialPort)
        {
            if(partialPort.name != null)
                currentPort.name = partialPort.name;
            if(partialPort.location != null) 
                currentPort.location = partialPort.location;
            if(partialPort.capacity != null)
                currentPort.capacity = partialPort.capacity;
            marlinBD.SaveChanges();
        }
        public void DeletePort(tbl_fishingport port)
        {
            marlinBD.tbl_fishingport.Remove(port);
            marlinBD.SaveChanges();
        }
    }
}