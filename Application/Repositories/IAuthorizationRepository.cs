using Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAuthorizationRepository
    {
        Task<bool> IsUserAuthorized(string token, string permission, string resource);
        Task<Tuple<string, string>> GetUserPermissions(string token);
        Task<UserDTO> LoginUser(string username, string password);
        Task<UserDTO> RegisterUser(string username, string password, string email);
        Task<bool> UpdateUserPermissions(string token, string permission, string resource);
    }
    public class AuthorizationRepository : IAuthorizationRepository
    {
        public Task<bool> IsUserAuthorized(string token, string permission, string resource)
        {
            throw new NotImplementedException();
        }
        public Task<Tuple<string, string>> GetUserPermissions(string token)
        {
            // Implementation for getting user permissions
            throw new NotImplementedException();
        }
        public Task<UserDTO> LoginUser(string username, string password)
        {
            // Implementation for user login
            throw new NotImplementedException();
        }
        public Task<UserDTO> RegisterUser(string username, string password, string email)
        {
            // Implementation for user registration
            throw new NotImplementedException();
        }
        public Task<bool> UpdateUserPermissions(string token, string permission, string resource)
        {
            // Implementation for updating user permissions
            throw new NotImplementedException();
        }
    }
}
