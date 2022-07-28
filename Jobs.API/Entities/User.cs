using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jobs.API.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public int RightToPublish { get; set; }
    }
}
