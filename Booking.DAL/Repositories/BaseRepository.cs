using Booking.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace Booking.DAL.Repositories
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
           if (entity == null) 
                throw new ArgumentNullException("Entity is null - " + nameof(entity));

            await _dbContext.AddAsync(entity);
           
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public IQueryable<TEntity> GetAllAsSplitQuery()
        {
            return _dbContext.Set<TEntity>().AsSplitQuery();
        }

        public TEntity Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null - " + nameof(entity));

            _dbContext.Remove(entity);
           
            return entity;
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("Entity is null - " + nameof(entities));

            _dbContext.RemoveRange(entities);

            return entities;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("Entity is null - " + nameof(entities));

            _dbContext.UpdateRange(entities);

            return entities;
        }

        public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("Entity is null - " + nameof(entities));

           await _dbContext.AddRangeAsync(entities);

            return entities;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null) 
                throw new ArgumentNullException("Entity is null - " + nameof(entity));

            _dbContext.Update(entity);
           
            return entity;
        }
    }
}
