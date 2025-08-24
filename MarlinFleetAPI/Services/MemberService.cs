using MarlinFleetAPI.Models;
using MarlinFleetAPI.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarlinFleetAPI.Services
{
    public class MemberService
    {
        private MemberRepository memberRepository;

        public MemberService()
        {
            memberRepository = new MemberRepository();
        }
        private bool ExistTripRole(string role)
        {
            return Enum.IsDefined(typeof(TripRole), role);
        }
        private void VerifyMemberAge(int age)
        {
            if (age <= 16 || age > 100)
            {
                throw new InvalidOperationException("Member's age is not valid");
            }
        }
        private void VerifyRole(string role)
        {
            if (!ExistTripRole(role)) 
                throw new InvalidOperationException($"The role {role} doesn't exists");
        }

        public List<tbl_member> ListAllMembers()
        {
            return memberRepository.GetAll();
        }
        public tbl_member FindMemberById(Guid uuiid) {
            return memberRepository.FindById(uuiid);
        }
        public tbl_member RegisterMember(tbl_member member)
        {
            member.registration_date = DateTime.Now;
            VerifyMemberAge(member.age);
            VerifyRole(member.role);    
            tbl_member created = memberRepository.Add(member);
            memberRepository.Save();
            return created;
        }
        public void UpdateMember(Guid uuid,tbl_member memberUpdated) {
            tbl_member member = memberRepository.FindById(uuid);

            if (member == null)
                throw new InvalidOperationException("Member Not Found: " + member.id);
            
            VerifyMemberAge(memberUpdated.age);
            VerifyRole(memberUpdated.role);

            member.role = memberUpdated.role;   
            member.age = memberUpdated.age; 
            member.name = memberUpdated.name;
            member.surnames = memberUpdated.surnames;
            member.email = memberUpdated.email;
            member.phone = memberUpdated.phone;

            memberRepository.Save();

        }
        public void DeleteMember(Guid uuid)
        {
            tbl_member member = memberRepository.FindById(uuid);
            if(member is null)
                throw new InvalidOperationException("Member Not Found: " + member.id);

            memberRepository.Remove(member);
            memberRepository.Save();
        }



    }
}