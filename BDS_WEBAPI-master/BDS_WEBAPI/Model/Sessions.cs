using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BDS_WEBAPI.Model
{
    public class Sessions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("Role")]
        public int Role { get; set; }
        [BsonElement("Username")]
        public string? Username { get; set; }
        [BsonElement("CreateDate")]
        public DateTime? CreateDate { get; set; }
        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }
        [BsonElement("EndTimeTime")]
        public DateTime ẺndTime { get; set; }
    }
}
