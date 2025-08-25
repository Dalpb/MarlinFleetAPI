using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public class FishingPortRepository : GenericRepository<tbl_fishingport>
    {
        public FishingPortRepository(MarlinFleetBDEntities context) : base(context)
        {
        }
    }
}