using AccountAPI.ViewModels;
using Application.DTO_s;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AccountAPI.Controllers
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUsersRepository _userRepository;
        private List<string> errors = new List<string>();
        public UserController(IUsersRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        private UserViewModel _userViewModel { get; set; } = new UserViewModel();
        // GET: UserController
        [HttpGet("{id}")]
        public ActionResult GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            try
            {
                if (user.UserId == 0)
                {
                    return NotFound(new
                    {
                        message = "User does not exist."
                    });
                }
                else if (user.Account.Select(account => account.Balance).Sum() > 0)
                {
                    return BadRequest(new
                    {
                        message = "User has a balance greater than zero. Cannot delete user."
                    });
                }
                else
                {
                    _userRepository.DeleteUser(userId);
                    return Ok(new
                    {
                        message = "User deleted successfully.",
                        user = new
                        {
                            UserId = user.UserId,
                            AccountId = user.Account.FirstOrDefault()?.AccountId ?? 0,
                            Success = true
                        }
                    });
                }
                ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {Id}", userId);
                return StatusCode(500, new
                {
                    message = "Failed to delete user. Try again later."
                });
            }
        }

        [HttpPost]
        public ActionResult CreateUser(int userId, double initialDeposit, int accountType)
        {
            var userViewModel = new UserViewModel
            {
                Id = userId,
                Account = new List<AccountViewModel>
                            {
                                new AccountViewModel
                                {
                                    AccountId = new Random().Next(),
                                    Balance = initialDeposit,
                                    CurrencyCode = "USD",
                                    AccountType = accountType == 1 ? "Checking" : "Savings"
                                }
                            }
            };
            try
            {
                UserDTO _u = UserViewModel.ToDTO(userViewModel);
                _userRepository.CreateUser(_u);
                if (_u.Success)
                {
                    var account = userViewModel.Account.FirstOrDefault();
                    return Ok(new
                    {
                        message = "User created successfully.",
                        user = new
                        {
                            Id = userId,
                            Account = new
                            {
                                AccountId = new Random().Next(),
                                Balance = initialDeposit,
                                CurrencyCode = "USD",
                                AccountType = account?.AccountType ?? "Unknown",
                                Success = true,
                            }
                        }
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "User creation failed.",
                        Success = false
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, new
                {
                    message = "Failed to create user. Try again later."
                });
            }
        }

    }
}
