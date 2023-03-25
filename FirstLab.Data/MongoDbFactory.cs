using FirstLab.Data.Interfaces;
using MongoDB.Driver;

namespace FirstLab.Data;

public class MongoDbFactory : IMongoDbFactory
{
    private readonly IMongoClient _client;

    public MongoDbFactory(string connectionString)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);
    }

    public IMongoCollection<T> GetCollection<T>(string databaseName, string collectionNme)
    {
        return _client.GetDatabase(databaseName).GetCollection<T>(collectionNme);
    }
}
