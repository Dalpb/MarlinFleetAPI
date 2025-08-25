using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public class BoatRepository : GenericRepository<tbl_boat>
    {
        public BoatRepository(MarlinFleetBDEntities db) : base(db) { 
        }
    }
}