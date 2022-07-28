using Jobs.API.Entities;
using MongoDB.Driver;

namespace Jobs.API.Data
{
    public class JobContextSeed
    {
        public static void SeedData(IMongoCollection<Job> jobCollection, IMongoCollection<User> userCollection, IMongoCollection<BadWord> badWordCollection)
        {
            bool existUser = userCollection.Find(p => true).Any();
            if (!existUser)
            {
                userCollection.InsertManyAsync(GetPreconfiguredUsers());
                jobCollection.InsertManyAsync(GetPreconfiguredJobs(GetPreconfiguredUsers().FirstOrDefault()));
                badWordCollection.InsertManyAsync(GetPreconfiguredBadWords());

            }
        }

        private static IEnumerable<Job> GetPreconfiguredJobs(User user)
        {
            return new List<Job>()
            {
                new Job()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Position = "Software Developer",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    Duration = "",
                    Salary = 950.00M,
                    Owner = user.Phone,
                    ReleaseDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(15),
                    Quality = 5,
                    SideRights = "Ticket",
                    WorkingType = "Full Time"
                }
            };
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Phone = "0123456789",
                    Address = "Software Developer",
                    Company = "Test Company",
                    RightToPublish = 2
                }
            };
        }

        private static IEnumerable<BadWord> GetPreconfiguredBadWords()
        {
            return new List<BadWord>()
            {
                new BadWord()
                {
                    Word = "dump",
                }
            };
        }
    }
}
