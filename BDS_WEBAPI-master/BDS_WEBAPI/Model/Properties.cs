using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BDS_WEBAPI.Model
{
    public class Propeties
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;
        [BsonElement("Title")]
        public string? Title { get; set; }
        [BsonElement("Description")]
        public string? Description { get; set; }
        [BsonElement("Price")]
        public Int64? Price { get; set; }
        [BsonElement("Address")]
        public string? Address { get; set; }
        [BsonElement("Images")]
        public string? Images { get; set; }
        [BsonElement("Status")]
        public int Status { get; set; }
        [BsonElement("CreateAt")]
        public DateTime? CreateAt { get; set; }
    }
}
