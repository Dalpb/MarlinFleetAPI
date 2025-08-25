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
    }
    public class UnitOfWork : IUnitOfWork
    {
        private MarlinFleetBDEntities context { get; } = new MarlinFleetBDEntities(); 
        private MemberRepository memberRepository;
        private BoatRepository boatRepository;
        private bool _disposed;
        public UnitOfWork(){
            
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