namespace Amx.Storage.Infrastructure.Data.EntityFramework
{
    using CodeChallenger.Lancamentos.Adapters.Repository.Context;
    using CodeChallenger.Lancamentos.Domain.Entity;
    using CodeChallenger.Lancamentos.Domain.Repository;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class ReadRepositoryEntityFramework : IReadRepository
    {
        #region Ctrs

        public ReadRepositoryEntityFramework(ChallengerDbContext context)
        {
            this._dbContext = context;
        }

        #endregion

        #region Attrs

        private readonly DbContext _dbContext;

        #endregion

        #region IReadRepository

        public async Task<T> GetByIdAsync<T>(int id) 
            where T : AbstractBaseEntity
        {
            if (id <= 0)
                return null!;

            return await this._dbContext
                .Set<T>()
                .FirstOrDefaultAsync(x => id.Equals(x.Id)) ?? null!;
        }

        public async Task<T> GetOneAsync<T>(Expression<Func<T, bool>> where)
            where T : AbstractBaseEntity

        {
            return await this._dbContext.Set<T>().FirstOrDefaultAsync(where) ?? null!;
        }

        public async Task<ICollection<T>> GetAllByIdAsync<T>(ICollection<int> ids)
            where T : AbstractBaseEntity
        {
            if (!(ids?.Any() ?? false))
                return [];

            var query = this._dbContext.Set<T>().Where(x => ids.Contains(x.Id)).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync<T>(params Expression<Func<T, bool>>[] where)
            where T : AbstractBaseEntity
        {
            var query = this._dbContext.Set<T>().AsQueryable();

            if (where?.Any() ?? false)
            {
                foreach (var predicate in where)
                {
                    query = query.Where(predicate);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync<T>(params Expression<Func<T, bool>>[] where)
            where T : AbstractBaseEntity
        {
            var query = this._dbContext.Set<T>().AsQueryable();

            if (where?.Any() ?? false)
            {
                foreach (var predicate in where)
                {
                    query = query.Where(predicate);
                }
            }

            return await query.CountAsync() > 0;
        }

        public Task<int> CountAsync<T>(params Expression<Func<T, bool>>[] where)
            where T : AbstractBaseEntity
        {
            var query = this._dbContext.Set<T>().AsQueryable();

            if (where?.Any() ?? false)
            {
                foreach (var predicate in where)
                {
                    query = query.Where(predicate);
                }
            }

            return query.CountAsync();
        }

        public IQueryable<T> GetQuery<T>()
            where T : AbstractBaseEntity
        {
            return this._dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetQuery<T>(params Expression<Func<T, bool>>[] where)
            where T : AbstractBaseEntity
        {
            var query = this._dbContext.Set<T>().AsQueryable();

            if (where?.Any() ?? false)
            {
                foreach(var predicate in where)
                {
                    query = query.Where(predicate);
                }
            }

            return query;
        }

        #endregion
    }
}
