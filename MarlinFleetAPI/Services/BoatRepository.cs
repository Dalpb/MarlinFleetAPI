using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Services
{
    //ver lo de singleton DB, RECORDAR, ver IDispose <- conexión sin administracion
    public class BoatRepository
    {
        private MarlinFleetBDEntities marlingBD;
        public BoatRepository() { 
            marlingBD = new MarlinFleetBDEntities();
        } 
        public List<tbl_boat> GetAll()
        {
            return marlingBD.tbl_boat.ToList();
        }
        public tbl_boat Add(tbl_boat boat)
        {
            return marlingBD.tbl_boat.Add(boat);
        }
        public tbl_boat FindById(Guid id) { 
            return marlingBD.tbl_boat.Find(id);
        }
        public tbl_boat Remove(tbl_boat boat) {
            return marlingBD.tbl_boat.Remove(boat);
        }
        public void Save() { 
            marlingBD.SaveChanges();
        }

    }
}