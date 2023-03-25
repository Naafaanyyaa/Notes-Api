using System.Linq.Expressions;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(string id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteByIdAsync(string id);
    Task DeleteAsync(TEntity entity);
}