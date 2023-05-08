using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BDS_WEBAPI.Model
{
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = string.Empty;
        [BsonElement("Title")]
        public string? Title { get; set; }
        [BsonElement("content")]
        public string? content { get; set; }
        [BsonElement("Date_Public")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTimeOffset DatePublic { get; set; }
        [BsonElement("By")]
        public string? By { get; set; }

    }
}
