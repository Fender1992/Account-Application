using Application.DTO_s;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUsersRepository
    {
        UserDTO CreateUser(UserDTO user);
        UserDTO GetUserById(int userId);
        UserDTO UpdateUser(UserDTO user);
        void DeleteUser(int userId);
    }
    public class UsersRepository : IUsersRepository
    {
        UsersAccounts UsersAccounts { get; set; } = new UsersAccounts();
        public UserDTO CreateUser(UserDTO user)
        {
            UsersAccounts.users.Add(user);
            user.Success = true;
            return user;
        }
        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            UsersAccounts.users.Remove(user);
        }
        public UserDTO GetUserById(int userId)
        {
            return UsersAccounts.users.FirstOrDefault(x => x.UserId == userId) ?? new UserDTO();
        }
        public UserDTO UpdateUser(UserDTO user)
        {
            var existingUser = UsersAccounts.users.FirstOrDefault(x => x.UserId == user.UserId);
            return new UserDTO();
        }
    }
}
