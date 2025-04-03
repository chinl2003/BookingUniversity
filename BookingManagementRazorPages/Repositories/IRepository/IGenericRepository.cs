using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(object id);
        void Save();
    }
}
