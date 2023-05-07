using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BDS_WEBAPI.Model
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("Role")]
        public int Role { get; set; }
        [BsonElement("Username")]
        public string? Username { get; set; }
        [BsonElement("Password")]
        public string? Password { get; set; }
        [BsonElement("Email")]
        public string? Email { get; set; }
        [BsonElement("Fullname")]
        public string? Fullname { get; set; }
        [BsonElement("Phone")]
        public Int64? Phone { get; set; }
        [BsonElement("CreateDate")]
        public DateTime? CreateDate { get; set; }
    }
}
