using Repositories.Entities;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private bool disposed = false;
        private readonly BookingManagementContext _dbContext;
        private IGenericRepository<User>? _userRepository;
        public UnitOfWork(BookingManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<User> UserRepository
             => _userRepository ??= new GenericRepository<User>(_dbContext);

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("DbContext is being disposed.");
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
