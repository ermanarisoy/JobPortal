using Jobs.API.Entities;
using MongoDB.Driver;

namespace Jobs.API.Data
{
    public class JobContext : IJobContext
    {
        public JobContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Jobs = database.GetCollection<Job>(configuration.GetValue<string>("DatabaseSettings:JobCollectionName"));
            Users = database.GetCollection<User>(configuration.GetValue<string>("DatabaseSettings:UserCollectionName"));
            BadWords = database.GetCollection<BadWord>(configuration.GetValue<string>("DatabaseSettings:BadWordCollectionName"));
            JobContextSeed.SeedData(Jobs, Users, BadWords);
        }
        public IMongoCollection<Job> Jobs { get; }
        public IMongoCollection<User> Users { get; }
        public IMongoCollection<BadWord> BadWords { get; }
    }
}
