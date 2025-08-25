using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public class TripRepository : GenericRepository<tbl_trip>
    {
        public TripRepository(MarlinFleetBDEntities bd) : base(bd)
        {

        }
    }
}