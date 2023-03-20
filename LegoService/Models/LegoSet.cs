using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LegoService.Models;

public class LegoSet
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("modelNumber")]
    public string ModelNumber { get; set; }
    [BsonElement("yearReleased")]
    public int YearReleased { get; set; }
    [BsonElement("pieceCount")]
    public int PieceCount { get; set; }
    [BsonElement("theme")]
    public string Theme { get; set; }
    [BsonElement("price")]
    public decimal Price { get; set; }
}