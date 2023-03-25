using MongoDB.Bson.Serialization.Attributes;
using SavePets.Data.Entities.Abstract;

namespace FirstLab.Data.Models;

public class Note : BaseEntity
{
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("text")]
    public string Text { get; set; }
    [BsonElement("userId")]
    public Guid UserId { get; set; }
}
