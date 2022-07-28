using Jobs.API.Entities;

namespace Jobs.API.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string id);
        Task CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(string id);

        //Task<IEnumerable<User>> GetUser();
        //Task<User> GetUser(string phone);
        //Task PutUser(User user);
        //Task<User> PostUser(User user);
        //Task DeleteUser(User user);
        //bool UserExists(string phone);
    }
}
