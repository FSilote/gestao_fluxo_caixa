namespace CodeChallenger.Saldo.Domain.Repository
{
    using CodeChallenger.Saldo.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : AbstractBaseEntity;

        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> where) where T : AbstractBaseEntity;

        Task<ICollection<T>> GetAllByIdAsync<T>(ICollection<int> ids) where T : AbstractBaseEntity;

        Task<ICollection<T>> GetAllAsync<T>(params Expression<Func<T, bool>>[] where) where T : AbstractBaseEntity;

        Task<bool> AnyAsync<T>(params Expression<Func<T, bool>>[] where) where T : AbstractBaseEntity;

        Task<int> CountAsync<T>(params Expression<Func<T, bool>>[] where) where T : AbstractBaseEntity;

        IQueryable<T> GetQuery<T>() where T : AbstractBaseEntity;

        IQueryable<T> GetQuery<T>(params Expression<Func<T, bool>>[] where) where T : AbstractBaseEntity;
    }
}