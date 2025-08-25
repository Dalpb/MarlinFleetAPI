using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public class FSpeciesRepository : GenericRepository<tbl_fspecies> 
    {
        public FSpeciesRepository(MarlinFleetBDEntities context) : base(context)
        {
        }
    }
}