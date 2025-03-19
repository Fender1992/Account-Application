using Application.DTO_s;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAccountRepository
    {
        AccountDTO GetAccountById(int userId, int accountId);
        double GetBalance(int userId);
        double UpdateBalance(string action, int userId, int accountId, double amount);
    }

    public class AccountRepository : IAccountRepository
    {
        UsersAccounts usersAccounts { get; set; } = new UsersAccounts();
        public AccountDTO GetAccountById(int userId, int accountId)
        {
            return usersAccounts.users
                .Where(x => x.UserId == userId)
                .SelectMany(e => e.Account)
                .FirstOrDefault(a => a.AccountId == accountId) ?? new AccountDTO();
        }
        public double GetBalance(int userId, int accountId)
        {
            var user = usersAccounts.users.FirstOrDefault(x => x.UserId == userId);
            
            if (user != null)
            {
                var account = user.Account.FirstOrDefault(x => x.AccountId == accountId);
                if (account != null)
                {
                    return account.Balance;
                }
            }
            return 0.0;
        }
        public UserDTO UpdateBalance(string action, int userId, int accountId, double amount)
        {
            double balance = GetBalance(accountId, userId);
            switch (action)
            {
                case "withdraw":
                    balance -= amount;
                    return usersAccounts.users.FirstOrDefault(x => x.UserId == userId) ?? new UserDTO();
                case "deposit":
                    balance += amount;
                    return usersAccounts.users.FirstOrDefault(x => x.UserId == userId) ?? new UserDTO();
                default:
                    return new UserDTO();
            }
        }
    }
}
