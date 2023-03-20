using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LegoService.Models;

public class LegoTheme
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
}