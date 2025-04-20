using AccountAPI.ViewModels;
using Application.DTO_s;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private List<string> errors = new List<string>();
        private IMapper _mapper;
        private UserViewModel _userViewModel = new UserViewModel();
        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: UserController
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            UserDTO? user = await _userService.GetUserById(id);
            return Ok(new
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Balance = user.Account.Select(x => x.Balance)
            });
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            UserDTO? user = await _userService.GetUserById(id);
            try
            {
                if (user == null || user.UserId <= 0)
                {
                    return BadRequest(new
                    {
                        Message = "Invalid user ID.",
                        Success = false
                    });
                }
                else
                {
                    await _userService.DeleteUser(user.UserId);
                    return Ok(new
                    {
                        Message = "User deleted successfully.",
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {Id}", user.UserId);
                return StatusCode(500, new
                {
                    Message = "Failed to delete user. Try again later."
                });
            }
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] UserViewModel user,
            double intialDeposit, string accountType)
        {
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Account = new List<AccountViewModel>
                {
                    new AccountViewModel
                    {
                        AccountId = new Random().Next(),
                        Balance = intialDeposit,
                        CurrencyCode = "USD",
                        AccountType = accountType,
                        AccountName = user.Account.FirstOrDefault()?.AccountName 
            }
                }
            };
            try
            {
                UserDTO userDto = _mapper.Map<UserViewModel, UserDTO>(userViewModel);
                _userService.CreateUser(userDto);
                if (userDto.Success)
                {
                    var account = userViewModel.Account.FirstOrDefault();
                    if (account == null)
                    {
                        return BadRequest(new
                        {
                            Message = "Account creation failed.",
                            Success = false
                        });
                    }
                    return Ok(new
                    {
                        Message = "User created successfully.",
                        Account = account,
                        Amount = intialDeposit,
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "User creation failed.",
                        Success = false
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, new
                {
                    Message = "Failed to create user. Try again later."
                });
            }
        }

    }
}
