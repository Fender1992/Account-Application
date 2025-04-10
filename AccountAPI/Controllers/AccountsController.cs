using AccountAPI.ViewModels;
using Application.DTO_s;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet("balance/{userId}")]
    public async Task<IActionResult> GetBalance(int userId)
    {
        try
        {
            double balance = await _accountService.GetBalance(userId);
            return Ok(new 
            {
                Message = $"Your current balance is {balance:C}.",
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("withdraw/{userId}")]
    public async Task<IActionResult> Withdraw(int userId, double amount)
    {
        try
        {
            double newBalance = await _accountService.WithdrawService(userId, amount);
            UserDTO user = await _userService.GetUserById(userId); 

            var userViewModel = new
            {
                Id = user.UserId,
                Success = true,
                AccountId = user.Account.Select(x => x.AccountId),
                Balance = newBalance,
            };

            return Ok(new 
            {
                Message = $"You have successfully withdrew {amount:C}. Your new balance is {newBalance:C}.",
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }
    [HttpPost("deposit/{userId}")]
    public async Task<IActionResult> Deposit(int userId, double amount)
    {
        try
        {
            double newBalance = await _accountService.DepositService(userId, amount);
            UserDTO user = await _userService.GetUserById(userId); // Assuming there is a method to get user details

            var userViewModel = new
            {
                Id = user.UserId,
                Success = true,
                AccountId = user.Account.Select(x => x.AccountId),
                Balance = newBalance,
            };

            return Ok(new 
            {
                Message = $"Balance updated successfully, Your new balance is {newBalance:C}.",
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating balance for user {Id}", userId);
            return StatusCode(500, "Internal Server Error");
        }
    }
}


