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
    [Route("api/user/[controller]")]
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
        public ActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUser([FromBody] UserViewModel user, int id)
        {
            UserDTO userDTO = _mapper.Map<UserViewModel, UserDTO>(user);
            var checkedUser = await _userService.GetUserById(id);
            try
            {
                if (checkedUser == null || checkedUser.UserId <= 0)
                {
                    return BadRequest(new TransactionViewModel
                    {
                        Message = "Invalid user ID.",
                        Success = false
                    });
                }
                else
                {
                    await _userService.DeleteUser(userDTO.UserId);
                    return Ok(new TransactionViewModel
                    {
                        Message = "User deleted successfully.",
                        Success = true
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {Id}", userDTO.UserId);
                return StatusCode(500, new TransactionViewModel
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
                Account = new List<AccountViewModel>
                {
                    new AccountViewModel
                    {
                        AccountId = new Random().Next(),
                        Balance = intialDeposit,
                        CurrencyCode = "USD",
                        AccountType = accountType
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
                        return BadRequest(new TransactionViewModel
                        {
                            Message = "Account creation failed.",
                            Success = false
                        });
                    }
                    return Ok(new TransactionViewModel
                    {
                        Message = "User created successfully.",
                        Success = true,
                        Account = account,
                        Amount = intialDeposit,
                    });
                }
                else
                {
                    return BadRequest(new TransactionViewModel
                    {
                        Message = "User creation failed.",
                        Success = false
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new TransactionViewModel
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, new TransactionViewModel
                {
                    Message = "Failed to create user. Try again later."
                });
            }
        }

    }
}
