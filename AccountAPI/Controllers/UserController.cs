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
        private IMapper _mapper;
        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: UserController
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserViewModel>>> GetUserById(int id)
        {
            UserDTO? user = await _userService.GetUserById(id);
            return Ok(ApiResponse<UserViewModel>.CreateResponse<UserViewModel>(true, "User retrieved successfully.", new UserViewModel()
            {
                Id = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            }));
        }
        [HttpGet("allUsers")]

        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUser(int id)
        {
            try
            {
                UserDTO? user = await _userService.GetUserById(id);
                if (user == null || user.UserId <= 0)
                {
                    return BadRequest(ApiResponse<string>.CreateResponse<string>(false, "User not found."));
                }
                else
                {
                    await _userService.DeleteUser(user.UserId);
                    return Ok(ApiResponse<string>.CreateResponse<string>(true, "User deleted successfully."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {Id}", id);
                return BadRequest(ApiResponse<string>.CreateResponse<string>(false, "Failed to delete user. Try again later."));

            }
        }

        [HttpPost("createUser")]
        public ActionResult<ApiResponse<AccountViewModel>> CreateUser([FromBody] UserViewModel user)
        {
            try
            {
                // Validate input
                if (user == null)
                    return BadRequest(ApiResponse<AccountViewModel>.CreateResponse<AccountViewModel>(false, "Invalid user data"));

                // Extract account data once
                var accountData = user.Account?.FirstOrDefault();
                var initialBalance = accountData?.Balance ?? 0;
                var accountType = accountData?.AccountType ?? "";
                var accountName = accountData?.AccountName ?? "";

                // Create a new account with proper initialization
                var newAccount = new AccountViewModel
                {
                    AccountId = new Random().Next(),
                    Balance = initialBalance,
                    CurrencyCode = "USD",
                    AccountType = accountType,
                    AccountName = accountName
                };

                // Create user with the new account
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Password = user.Password,
                    Account = new List<AccountViewModel> { newAccount }
                };

                // Map and create user
                UserDTO userDto = _mapper.Map<UserViewModel, UserDTO>(userViewModel);
                _userService.CreateUser(userDto);

                if (!userDto.Success)
                    return BadRequest(ApiResponse<AccountViewModel>.CreateResponse<AccountViewModel>(false, "User creation failed."));

                // Return success response with account info
                return Ok(ApiResponse<AccountViewModel>.CreateResponse(
                    true,
                    "User created successfully.",
                    newAccount
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<AccountViewModel>.CreateResponse<AccountViewModel>(false, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, ApiResponse<AccountViewModel>.CreateResponse<AccountViewModel>(
                    false,
                    "Failed to create user. Try again later."
                ));
            }
        }

    }
}
