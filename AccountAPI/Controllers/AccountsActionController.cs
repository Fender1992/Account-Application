using AccountAPI.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsActionController : ControllerBase
{
    private readonly ILogger<AccountsActionController> _logger;
    private readonly IAccountRepository _accountService;

    public AccountsActionController(ILogger<AccountsActionController> logger, IAccountRepository accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetBalance(int userId)
    {
        try
        {
            double balance = _accountService.GetBalance(userId);
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
            double updatedBalance = _accountService.UpdateBalance(action, accountId, userId, amount);
            var user = _accountService.GetAccountById(userId, accountId);

            var userViewModel = new UserViewModel
            {
                Id = user.User.UserId,
                Success = true,
                Account = new List<AccountViewModel>
            {
                new AccountViewModel
                {
                    AccountId = user.AccountId,
                    Balance = updatedBalance
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
}

