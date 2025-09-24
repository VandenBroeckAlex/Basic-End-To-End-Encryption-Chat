using stamp_back.Data;
using stamp_back.Interfaces;
using stamp_back.Models;

namespace stamp_back.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) => this._context = context;

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(p => p.Email == email).FirstOrDefault();
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public User GetUserByName(string username)
        {
            return _context.Users.Where(p => p.UserName == username).FirstOrDefault();
        }


        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(x => x.Id).ToList();
        }


        public ICollection<User> GetUsers(string username)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(Guid id)
        {
            return _context.Users.All(x => x.Id == id);
        }
    }

    
}
