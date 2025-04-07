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
        void CreateUser(UserDTO user);
        void UpdateUser(int userId, string username, string password);
        void DeleteUser(int userId);
        UserDTO GetUserById(int userId);
    }
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        private readonly Mapper _userMapper;
        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //var userMapper = _userMapper.Map<UserViewModel, UserDTO>(user);

        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int userId, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
