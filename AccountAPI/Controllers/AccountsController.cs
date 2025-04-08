using AccountAPI.ViewModels;
using Application.DTO_s;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("api/account/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private List<string> errors = new List<string>();
    public AccountsController(ILogger<AccountsController> logger, IAccountService accountService, IUserService userService)
    {
        _logger = logger;
        _accountService = accountService;
        _userService = userService;
    }

    [HttpGet("balance/{userId}/{accountId}")]
    [Authorize]
    public async Task<IActionResult> GetBalance(int userId, int accountId)
    {
        try
        {
            double balance = await _accountService.GetBalance(userId, accountId);
            return Ok(balance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("withdraw/{userId}/{accountId}")]
    public async Task<IActionResult> Withdraw(int userId, int accountId, double amount)
    {
        try
        {
            double newBalance = await _accountService.Withdraw(userId, accountId, amount);
            UserDTO user = await _userService.GetUserById(userId); // Assuming there is a method to get user details

            var userViewModel = new
            {
                Id = user.UserId,
                Success = true,
                AccountId = user.Account.FirstOrDefault(x => x.AccountId == accountId)?.AccountId ?? 0,
                Balance = newBalance,
            };

            return Ok(new TransactionViewModel
            {
                Message = "Balance updated successfully.",
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }
}


