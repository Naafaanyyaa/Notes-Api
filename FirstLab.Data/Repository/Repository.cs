using System.Linq.Expressions;
using FirstLab.Data.Interfaces;
using FirstLab.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using SavePets.Data.Entities.Abstract;
using SavePets.Data.Interfaces;

namespace FirstLab.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _entity;

    public Repository(IMongoDbFactory mongoDbFactory, string table)
    {
        _entity = mongoDbFactory.GetCollection<TEntity>("lab_work", $"{table}");
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        using var result = await _entity.FindAsync(new BsonDocument());
        return await result.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        using var results = await _entity.FindAsync(x => x.Id == id);
        return await results.FirstOrDefaultAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _entity.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(string id)
    {
        await _entity.DeleteOneAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}