using System.Linq.Expressions;
using FirstLab.Data.Interfaces;
using FirstLab.Data.Models;
using MongoDB.Driver;
using SavePets.Data.Entities.Abstract;
using SavePets.Data.Interfaces;

namespace FirstLab.Data.Repository;

public class UserRepository :Repository<User>, IUserRepository
{
 
    private readonly IMongoCollection<User> _entity;

    public UserRepository(IMongoDbFactory mongoDbFactory) : base (mongoDbFactory, "users")
    {
        _entity = mongoDbFactory.GetCollection<User>("lab_work", "users");
    }

    public async Task<User?> GetByLogin(string login)
    {
        using var results = await _entity.FindAsync(x => x.Name == login);


        return await results.FirstOrDefaultAsync();
    }

    public override async Task<User> UpdateAsync(User entity)
    {
        await _entity.FindOneAndUpdateAsync(x => x.Id == entity.Id, Builders<User>.Update.Set(x => x.LastModifiedDate, entity.LastModifiedDate)).ConfigureAwait(false);
        var result = await _entity.FindOneAndUpdateAsync(x => x.Id == entity.Id, Builders<User>.Update.Set(x => x.Password, entity.Password)).ConfigureAwait(false);
        

        return result;
    }
}