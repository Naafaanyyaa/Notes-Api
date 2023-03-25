using MongoDB.Bson.Serialization.Attributes;

namespace SavePets.Data.Entities.Abstract;

public class BaseEntity
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonElement("firstUpd")]
    public DateTime CreatedDate { get; set; }
    [BsonElement("lastUpd")]
    public DateTime? LastModifiedDate { get; set; }
}