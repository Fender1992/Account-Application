using AccountAPI.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class AccountsActionController : ControllerBase
{
    private readonly ILogger<AccountsActionController> _logger;
    private readonly IAccountRepository _accountService;

    public AccountsActionController(ILogger<AccountsActionController> logger, IAccountRepository accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet("{id}")]
    public double GetBalance(int userId)
    {
        return _accountService.GetBalance(userId);
    }
    [HttpPut("{id}")]
    public double UpdateBalance(string action, int id, double amount)
    {
        switch(action)
        {
            case "withdraw":
                return _accountService.UpdateBalance("withdraw", id, amount);
            case "deposit":
                return _accountService.UpdateBalance("deposit", id, amount);
            default:
                return _accountService.UpdateBalance("deposit", id, amount);
        }
    }
}
