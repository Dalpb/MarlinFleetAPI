using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    //igual q sprningboot xD
    public class MemberRepository
    {
        private MarlinFleetBDEntities marlinBD;
        public MemberRepository() { 
            marlinBD = new MarlinFleetBDEntities();
        }

        public List<tbl_member> GetAll()
        {
            return marlinBD.tbl_member.ToList();
        }
        public tbl_member FindById(Guid uuid)
        {
            return marlinBD.tbl_member.Find(uuid);
        }
        public tbl_member Add(tbl_member member)
        {
            return marlinBD.tbl_member.Add(member);
        }
        public tbl_member Remove(tbl_member member)
        {
            return marlinBD.tbl_member.Remove(member);
        }
        public void Save()
        {
            marlinBD.SaveChanges();
        }


    }
}