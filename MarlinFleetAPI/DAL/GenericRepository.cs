using MarlinFleetAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using System.Web;

namespace MarlinFleetAPI.DAL
{
    interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> FindById(Guid id);
        T Insert(T entity);
        void Delete(T entity);   
        //void Update(T entity);
    }
    //aprendido en la doc de microsft
    public class GenericRepository<T> : IGenericRepository <T> where T : class 
    {
        private MarlinFleetBDEntities context;
        protected DbSet<T> bdset;

        public GenericRepository(MarlinFleetBDEntities context)
        {
            this.context = context;
            bdset = context.Set<T>(); //es como el acceso a la tabla que quiero
        }

        public async Task<T> FindById(Guid id)
        {
            return await bdset.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await bdset.ToListAsync();
        }

        public T Insert(T entity)
        {
            return bdset.Add(entity);
        }

        public void Delete(T entity)
        {
            bdset.Remove(entity);
        }

     /*   public void Update(T entity)
        {
            bdset.Attach(entity);
        }*/
    }
}