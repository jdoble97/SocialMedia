using SocialMediaCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }
}