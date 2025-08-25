using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public class CatchRepository : GenericRepository<tbl_catch>
    {
        public CatchRepository(MarlinFleetBDEntities context) : base(context)
        {
        }
    }
}