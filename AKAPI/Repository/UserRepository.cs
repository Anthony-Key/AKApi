using AKAPI.Data;
using AKAPI.Models;
using AKAPI.Repository.Interfaces;

namespace AKAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext db)
        {
            _context = db;
        }

        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
