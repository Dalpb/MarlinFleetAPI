using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MarlinFleetAPI.Repository
{
    //igual q sprningboot xD
    public class MemberRepository : GenericRepository<tbl_member>
    {
        public MemberRepository(MarlinFleetBDEntities db) : base(db) { 
        }

        public async Task<tbl_member> FindByNameAsync(string surname,string name)
        {
            return await bdset.FirstOrDefaultAsync(m => m.surnames.Equals(surname) && m.name.Equals(name));
        }

    }
}