using Exercise1.DataLayer.Models;

namespace Exercise1.DataLayer.Repositories
{
    public interface IUserRepository
    {
        User GetUser(int id);
        List<User> GetUsers();
        int AddUser(User user);
        int UpdateUser(User user);
    }
}
