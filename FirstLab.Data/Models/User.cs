using MongoDB.Bson.Serialization.Attributes;
using SavePets.Data.Entities.Abstract;

namespace FirstLab.Data.Models;

public class User : BaseEntity
{
    [BsonElement("title")]
    public string Name { get; set; }
    [BsonElement("text")]
    public string Password { get; set; }
}
