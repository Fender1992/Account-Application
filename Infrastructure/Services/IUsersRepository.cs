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
        UsersAccounts usersAccounts { get; set; } = new UsersAccounts();
        public UserDTO CreateUser(UserDTO user)
        {
            usersAccounts.users.Add(user);
            return user;
        }
        public void DeleteUser(int userId)
        {
            var user = usersAccounts.users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                usersAccounts.users.Remove(user);
            }
        }
        public UserDTO GetUserById(int userId)
        {
            return usersAccounts.users.FirstOrDefault(x => x.UserId == userId) ?? new UserDTO();
        }
        public UserDTO UpdateUser(UserDTO user)
        {
            var existingUser = usersAccounts.users.FirstOrDefault(x => x.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                return existingUser;
            }
            return new UserDTO();
        }
    }
}
