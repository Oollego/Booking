using Booking.Domain.Interfaces.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>: IStateSaveChanges
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);
        IQueryable<TEntity> GetAllAsSplitQuery();
    }
}
