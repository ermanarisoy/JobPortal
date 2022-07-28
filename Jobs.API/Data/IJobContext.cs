using Jobs.API.Entities;
using MongoDB.Driver;

namespace Jobs.API.Data
{
    public interface IJobContext
    {
        IMongoCollection<Job> Jobs { get; }
        IMongoCollection<User> Users { get; }
        IMongoCollection<BadWord> BadWords { get; }
    }
}
