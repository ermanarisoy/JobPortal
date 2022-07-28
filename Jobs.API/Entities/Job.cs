using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jobs.API.Entities
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quality { get; set; }
        public string? SideRights { get; set; }
        public string? WorkingType { get; set; }
        public decimal? Salary { get; set; }
        public string Owner { get; set; }
    }
}
