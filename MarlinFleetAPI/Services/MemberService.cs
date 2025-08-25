using MarlinFleetAPI.Models;
using MarlinFleetAPI.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarlinFleetAPI.Services
{
    public class MemberService
    {
        private IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        private  void VerifyExistMemberByName(string surname,string name) { 
            var member =  _unitOfWork.MemberRepository.FindByName(surname, name);
            if(member != null)
                throw new InvalidOperationException("This member has been alredy registerd");
        }

        public Task<List<tbl_member>> ListAllMembers()
        {
            return _unitOfWork.MemberRepository.GetAll();
        }
        public Task<tbl_member> FindMemberById(Guid uuiid) {
            return  _unitOfWork.MemberRepository.FindById(uuiid);
        }
        public async Task<tbl_member> RegisterMember(tbl_member member)
        {
            VerifyExistMemberByName(member.surnames,member.name);
            VerifyMemberAge(member.age);
            VerifyRole(member.role);    

            member.registration_date = DateTime.Now;
            tbl_member created = _unitOfWork.MemberRepository.Insert(member);
            await _unitOfWork.SaveChangesAsync();
            return created;
        }
        public async Task UpdateMember(Guid uuid,tbl_member memberUpdated) {
            tbl_member member = await _unitOfWork.MemberRepository.FindById(uuid);
            if (member == null)
                throw new KeyNotFoundException("Member Not Found: " + member.id);
            
            VerifyMemberAge(memberUpdated.age);
            VerifyRole(memberUpdated.role);

            member.role = memberUpdated.role;   
            member.age = memberUpdated.age; 
            member.name = memberUpdated.name;
            member.surnames = memberUpdated.surnames;
            member.email = memberUpdated.email;
            member.phone = memberUpdated.phone;

            await _unitOfWork.SaveChangesAsync();

        }
        public async Task DeleteMember(Guid uuid)
        {
            tbl_member member = await _unitOfWork.MemberRepository.FindById(uuid);
            if (member is null)
                throw new KeyNotFoundException("Member Not Found: " + member.id);

            _unitOfWork.MemberRepository.Delete(member);
            await _unitOfWork.SaveChangesAsync();
        }



    }
}