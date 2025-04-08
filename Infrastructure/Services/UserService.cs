using AccountAPI.Mapper;
using Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        // Define methods for user-related operations
        Task<UserDTO> CreateUser(UserDTO user);
        Task<UserDTO?> UpdateUser(UserDTO user);
        Task DeleteUser(int userId);
        Task<UserDTO> GetUserById(int userId); // Fix: Corrected method signature
    }
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            var currentUser = _userRepository.GetUserById(user.UserId);
            if (currentUser.Id != 0)
                throw new ArgumentException("User already exists");
            else
                await Task.Run(() => _userRepository.CreateUser(user));
            return user;
        }

        public async Task DeleteUser(int userId)
        {
            await Task.Run(() => _userRepository.DeleteUser(userId));
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user;
        }

        public async Task<UserDTO?> UpdateUser(UserDTO user)
        {
            var currentUser = await _userRepository.GetUserById(user.UserId);
            if (currentUser == null)
                throw new ArgumentNullException(nameof(user));
            user.FirstName = currentUser.FirstName;
            user.LastName = currentUser.LastName;
            user.Password = currentUser.Password;
            return await _userRepository.UpdateUser(user);
        }
    }
}
