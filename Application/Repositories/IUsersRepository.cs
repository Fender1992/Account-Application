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
        Task<UserDTO?> GetUserById(int userId);
        Task<UserDTO?> UpdateUser(UserDTO user);
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
        public async void DeleteUser(int userId)
        {
            var user = await GetUserById(userId);
            if (user != null)
            {
                UsersAccounts.users.Remove(user);
            }
        }
        public async Task<UserDTO?> GetUserById(int userId)
        {
            var user = UsersAccounts.users.FirstOrDefault(x => x.UserId == userId);
            return await Task.FromResult(user);
        }
        public async Task<UserDTO?> UpdateUser(UserDTO user)
        {
            var existingUser = UsersAccounts.users.FirstOrDefault(x => x.UserId == user.UserId);
            return await Task.FromResult(existingUser);
        }
    }
}
