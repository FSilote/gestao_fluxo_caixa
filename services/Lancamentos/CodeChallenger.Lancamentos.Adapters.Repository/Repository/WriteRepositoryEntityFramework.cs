namespace Amx.Storage.Infrastructure.Data.EntityFramework
{
    using CodeChallenger.Lancamentos.Adapters.Repository.Context;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WriteRepositoryEntityFramework : IWriteRepository
    {
        #region Ctrs

        public WriteRepositoryEntityFramework(ChallengerDbContext context)
        {
            _dbContext = context;
        }

        #endregion

        #region Attrs

        private readonly DbContext _dbContext;

        #endregion

        #region IWriteRepository

        public async Task<T> InsertAsync<T>(T entity)
            where T : AbstractBaseEntity
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<ICollection<T>> InsertAsync<T>(ICollection<T> entities)
            where T : AbstractBaseEntity
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }
                
        public Task UpdateAsync<T>(T entity)
            where T : AbstractBaseEntity
        {
            return Task.Run(() => _dbContext.Set<T>().Update(entity));
        }

        public Task UpdateAsync<T>(ICollection<T> entities)
            where T : AbstractBaseEntity
        {
            return Task.Run(() => _dbContext.Set<T>().UpdateRange(entities));
        }

        public async Task<bool> DeleteAsync<T>(object id)
            where T : AbstractBaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync<T>(T entity)
            where T : AbstractBaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync<T>(ICollection<T> entities)
            where T : AbstractBaseEntity
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task SaveOrUpdateAsync<T>(T entity)
            where T : AbstractBaseEntity
        {
            EntityState state = _dbContext.Set<T>().Entry(entity).State;

            if (state == EntityState.Detached)
            {
                await _dbContext.Set<T>().AddAsync(entity);
            } 
            else if (state == EntityState.Modified)
            {
                _dbContext.Set<T>().Update(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveOrUpdateAsync<T>(ICollection<T> entities)
            where T : AbstractBaseEntity
        {
            var dbSet = _dbContext.Set<T>();

            foreach (var entity in entities)
            {
                EntityState state = dbSet.Entry(entity).State;

                if (state == EntityState.Detached)
                {
                    await _dbContext.Set<T>().AddAsync(entity);
                }
                else if (state == EntityState.Modified)
                {
                    _dbContext.Set<T>().Update(entity);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public Task FlushAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
