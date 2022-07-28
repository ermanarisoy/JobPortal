using Jobs.API.Data;
using Jobs.API.Interface;
using Jobs.API.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Jobs.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IJobContext _context;

        public UserRepository(IJobContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateUser(Entities.User user)
        {
            user.RightToPublish = 2;
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> DeleteUser(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(p => p.Phone, id);

            DeleteResult deleteResult = await _context
                                                .Users
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Entities.User> GetUser(string id)
        {
            return await _context
                           .Users
                           .Find(p => p.Phone == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.User>> GetUsers()
        {
            return await _context
                            .Users
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateUser(Entities.User user)
        {
            var updateResult = await _context
                                        .Users
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }


        #region
        //private readonly JobsAPIContext _context;
        //public UserRepository(JobsAPIContext context)
        //{
        //    _context = context;
        //}

        //public async Task DeleteUser(User user)
        //{
        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<User>> GetUser()
        //{
        //    return await _context.User.ToListAsync();
        //}

        //public async Task<User> GetUser(string phone)
        //{
        //    return await _context.User.FindAsync(phone);
        //}

        //public async Task<User> PostUser(User user)
        //{
        //    user.RightToPublish = 2;
        //    _context.User.Add(user);
        //    await _context.SaveChangesAsync();
        //    return user;
        //}

        //public async Task PutUser(User user)
        //{
        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public bool UserExists(string phone)
        //{
        //    return (_context.User?.Any(e => e.Phone == phone)).GetValueOrDefault();
        //}
        #endregion
    }
}
