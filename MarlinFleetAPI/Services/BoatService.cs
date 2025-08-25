using MarlinFleetAPI.Models;
using MarlinFleetAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MarlinFleetAPI.Services
{
    public class BoatService
    {
        private IUnitOfWork unitOfWork;

        public BoatService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<List<tbl_boat>> ListAllBoats()
        {
            return unitOfWork.BoatRepository.GetAll();
        }
        public async Task<tbl_boat> FindBoatByUUID(Guid uuid)
        {
            tbl_boat boat =  await unitOfWork.BoatRepository.FindById(uuid);

            if(boat is null)
                throw new KeyNotFoundException("Boat Not Found : " + uuid);
            else 
                return boat;
        }

        public async Task<tbl_boat> RegisterBoat(tbl_boat boat)
        {
            if (boat.id_fishingport == Guid.Empty )
                throw new ArgumentNullException("It Has not been asigned to a fishingport ");

            tbl_fishingport port  = await unitOfWork.FishingPortRepository.FindById(boat.id_fishingport);  

            if(port is null)
                throw new InvalidOperationException("The FishingPort doesn't exist: "+ boat.id_fishingport);

            TestApropiateBoat(boat);

            boat.created_at = DateTime.Now;
            boat.updated_at = boat.created_at;

            tbl_boat nboat = unitOfWork.BoatRepository.Insert(boat);
            await unitOfWork.SaveChangesAsync();
            return nboat;
        }

        public bool VerifyApropiateStatus(string status)
        {
            return Enum.IsDefined(typeof(BoatState), status);
        }
        public bool VerifyApropiateReg(string reg)
        {
            //debo pasarlo a regex XD
            return reg.Length == 12 && reg.Contains("-") && reg.Split('-').Length == 5; 
        }

        
        public void TestApropiateBoat(tbl_boat boat)
        {
            if (boat.displacement_tons < 0)
                throw new InvalidOperationException("Invalid displacement tons: " + boat.displacement_tons);

            if (!VerifyApropiateStatus(boat.status))
                throw new InvalidOperationException("Invalid Status: " + boat.status);

            if (VerifyApropiateReg(boat.registration))
                throw new InvalidOperationException("Invalid Registration: " + boat.registration);
        }





    }
}