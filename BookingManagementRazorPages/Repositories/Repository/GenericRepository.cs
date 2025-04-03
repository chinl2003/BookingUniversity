using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly BookingManagementContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(BookingManagementContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> Entities => _dbSet;

        public void Delete(object id)
        {
            TEntity? entity = _dbSet.Find(id);
            if (entity != null)
            {
                entity.GetType().GetProperty("DeletedAt")?.SetValue(entity, DateTime.Now);

                _dbSet.Update(entity);
            }
            else
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
        }

        public void Insert(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbSet.Entry(obj).State = EntityState.Modified;
        }
    }
}
