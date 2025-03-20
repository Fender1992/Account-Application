using AccountAPI.ViewModels;
using Application.DTO_s;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsActionController : ControllerBase
{
    private readonly ILogger<AccountsActionController> _logger;
    private readonly IAccountRepository _accountService;
    private readonly IUsersRepository _userRepository;
    private UserViewModel userViewModel { get; set; } = new UserViewModel();

    public AccountsActionController(ILogger<AccountsActionController> logger, IAccountRepository accountService, IUsersRepository userRepository)
    {
        _logger = logger;
        _accountService = accountService;
        _userRepository = userRepository;
    }

    [HttpGet("{userId}")]
    public IActionResult GetBalance(int userId, int accountId)
    {
        try
        {
            double balance = _accountService.GetBalance(userId, accountId);
            return Ok(balance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateBalance(int userId, int accountId, [FromQuery] string action, [FromQuery] double amount)
    {
        try
        {
            UserDTO user = _accountService.UpdateBalance(action, userId, accountId, amount);
            var _u = UserViewModel.ToViewModel(user);

            var userViewModel = new UserViewModel
            {
                Id = user.UserId,
                Success = true,
                Account = new List<AccountViewModel>
                {
                    new AccountViewModel
                    {
                        AccountId = user.Account.FirstOrDefault(x => x.AccountId == accountId)?.AccountId ?? 0,
                        Balance = user.Account.FirstOrDefault(x => x.AccountId == accountId)?.Balance ?? 0,
                    }
                }
            };

            return Ok(userViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }
}


