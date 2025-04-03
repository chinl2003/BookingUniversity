using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IGenericRepository<User> UserRepository { get; }
    }
}
