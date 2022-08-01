using AKAPI.Data;
using AKAPI.Models;
using AKAPI.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AKAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appsettings)
        {
            _context = db;
            _appSettings = appsettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x=> x.Name == username && x.Password == password);

            if(user == null)
            {
                return null;
            }

            var token = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials= new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var t = token.CreateToken(tokenDescriptor);

            user.Token = token.WriteToken(t);

            return user;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _context.Users.SingleOrDefault(x => x.Name == username);

            if (user == null)
            {
                return true;
            }

            return false;
        }

        public User Register(string username, string password)
        {
            var user = new User()
            {
                Name = username,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
