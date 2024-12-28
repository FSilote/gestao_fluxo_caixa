namespace CodeChallenger.Saldo.Domain.Repository
{
    using CodeChallenger.Saldo.Domain.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWriteRepository
    {
        Task<T> InsertAsync<T>(T entity) where T : AbstractBaseEntity;

        Task<ICollection<T>> InsertAsync<T>(ICollection<T> entities) where T : AbstractBaseEntity;

        Task UpdateAsync<T>(T entity) where T : AbstractBaseEntity;

        Task UpdateAsync<T>(ICollection<T> entities) where T : AbstractBaseEntity;

        Task<bool> DeleteAsync<T>(object id) where T : AbstractBaseEntity;

        Task<bool> DeleteAsync<T>(T entity) where T : AbstractBaseEntity;

        Task<bool> DeleteAsync<T>(ICollection<T> entities) where T : AbstractBaseEntity;

        Task SaveOrUpdateAsync<T>(T entity) where T : AbstractBaseEntity;

        Task SaveOrUpdateAsync<T>(ICollection<T> entities) where T : AbstractBaseEntity;

        Task FlushAsync();
    }
}
