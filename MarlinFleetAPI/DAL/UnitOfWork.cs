using MarlinFleetAPI.DAL;
using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarlinFleetAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        MemberRepository MemberRepository { get; }
        BoatRepository BoatRepository { get; }
        FishingPortRepository FishingPortRepository { get; }
        TripRepository TripRepository { get; }
        FSpeciesRepository FSpeciesRepository { get; }
        
    }
    public class UnitOfWork : IUnitOfWork
    {
        private MarlinFleetBDEntities context { get; } = new MarlinFleetBDEntities(); 
        private MemberRepository memberRepository;
        private BoatRepository boatRepository;
        private FishingPortRepository fishingPortRepository;
        private TripRepository tripRepository;
        private FSpeciesRepository fspeciesRepository;

        private bool _disposed;
        public UnitOfWork(){
            
        }

        public FishingPortRepository FishingPortRepository
        {
            get
            {
                if (this.fishingPortRepository == null)
                {
                    this.fishingPortRepository = new FishingPortRepository(context);
                }
                return this.fishingPortRepository;
            }
        }
        public TripRepository TripRepository
        {
            get
            {
                if (this.tripRepository == null)
                {
                    this.tripRepository = new TripRepository(context);
                }
                return this.tripRepository;
            }
        }
        public FSpeciesRepository FSpeciesRepository
        {
            get
            {
                if (this.fspeciesRepository == null) {
                    this.fspeciesRepository = new FSpeciesRepository(context);
                }
                return this.fspeciesRepository;
            }
        }

        public MemberRepository MemberRepository
        {
            get
            {
                if(this.memberRepository == null)
                {
                    this.memberRepository = new MemberRepository(context);
                }
                return this.memberRepository;
            }
        }
        public BoatRepository BoatRepository
        {
            get
            {
                if(this.boatRepository == null)
                {
                    this.boatRepository = new BoatRepository(context);
                }
                return this.boatRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) {
            if (!this._disposed)
                if(disposing)context.Dispose();
            this._disposed = true;
        }

        public  Task<int> SaveChangesAsync()
        {
           return  context.SaveChangesAsync();
        }
    }
}