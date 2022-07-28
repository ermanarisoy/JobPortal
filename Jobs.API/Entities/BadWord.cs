using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jobs.API.Entities
{
    public class BadWord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Word { get; set; }
    }
}
