using AccountAPI.ViewModels;
using Application.DTO_s;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("api/account/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IAccountRepository _accountService;
    private readonly IUsersRepository _userRepository;
    private List<string> errors = new List<string>();
    public AccountsController(ILogger<AccountsController> logger, IAccountRepository accountService, IUsersRepository userRepository)
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
    [ValidateAntiForgeryToken]
    public IActionResult UpdateBalance(int userId, int accountId, [FromQuery] string action, [FromQuery] double amount)
    {
        if (userId <= 0)
            errors.Add("User ID must be greater than zero.");

        if (accountId <= 0)
            errors.Add("Account ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(action) || !(action == "deposit" || action == "withdraw"))
            errors.Add("Action must be either 'deposit' or 'withdraw'.");

        if (amount <= 0)
            errors.Add("Amount must be greater than zero.");

        if (errors.Any())
            return BadRequest(new { errors });

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            UserDTO user = _accountService.UpdateBalance(action, userId, accountId, amount);
            var _u = UserViewModel.ToViewModel(user);

            var userViewModel = new 
            {
                Id = user.UserId,
                Success = true,
                ccountId = user.Account.FirstOrDefault(x => x.AccountId == accountId)?.AccountId ?? 0,
                Balance = _accountService.GetBalance(userId, accountId),
                
            };

            return Ok(new
            {
                message = "Balance updated successfully.",
                user = userViewModel
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }
}


