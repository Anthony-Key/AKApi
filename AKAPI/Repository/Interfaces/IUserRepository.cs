using AKAPI.Models;

namespace AKAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username); 
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
