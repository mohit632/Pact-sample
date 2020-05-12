using System.Linq;
using User.Postgre.Interfaces;
using User.Postgre.Models;

namespace User.Postgre.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public Models.User GetUserById(int id)
        {
            return _context.User.FirstOrDefault(p => p.Id == id);
        }
    }
}
