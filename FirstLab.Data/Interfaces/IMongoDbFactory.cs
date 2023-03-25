using MongoDB.Driver;

namespace FirstLab.Data.Interfaces;

public interface IMongoDbFactory
{
    IMongoCollection<T> GetCollection<T>(string databaseName, string collectionNme);
}
