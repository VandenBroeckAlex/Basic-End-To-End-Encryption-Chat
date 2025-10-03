using stamp_back.Dto;
using stamp_back.Models;

namespace stamp_back.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        ICollection<User> GetUsers(string username);
        User GetUserById(Guid id);
        User GetUserByName(string username);
        User GetUserByEmail(string email);
        bool UserExist(Guid id);     
        
        User PostUser(User user);
    }
    //get user by name
    //get user by mail
    //get user by id
}
