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
        Task<int> GetAccountIdByUserId(int userId);
        Task<double> GetBalance(int userId);
        Task<double> Withdraw(int userId, double amount);
        Task<double> Deposit(int userId, double amount);
        Task DeleteAccount(int userId);
    }

    public class AccountRepository : IAccountRepository
    {
        protected readonly IUsersRepository usersRepository;
        UsersAccounts usersAccounts { get; set; } = new UsersAccounts();
        public AccountRepository(IUsersRepository _usersRepository)
        {
            usersRepository = _usersRepository;
        }
        public async Task DeleteAccount(int userId)
        {
            var user = usersAccounts.users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                usersAccounts.users.Remove(user);
            }
        }
        public async Task<int> GetAccountIdByUserId(int userId)
        {
            UserDTO? user = await usersRepository.GetUserById(userId);
            if (user != null)
            {
                var account = user.Account.FirstOrDefault();
                if (account != null)
                {
                    return account.AccountId;
                }
            }
            return (0);
        }
        public async Task<double> GetBalance(int userId)
        {
            UserDTO? user = await usersRepository.GetUserById(userId);
            int _accountId = await GetAccountIdByUserId(userId);
            if (user != null)
            {
                var account = user.Account.FirstOrDefault(x => x.AccountId == _accountId);
                if (account != null)
                {
                    return account.Balance;
                }
            }
            return (0.0);
        }
        public async Task<double> Withdraw(int userId, double amount)
        {
            int _accountId = await GetAccountIdByUserId(userId);
            UserDTO? user = await usersRepository.GetUserById(userId); 
            if (user != null && _accountId != 0)
            {
                var account = user.Account.FirstOrDefault(x => x.AccountId == _accountId);
                if (account != null)
                {
                    account.Balance -= amount;
                    return account.Balance;
                }
            }
            return await GetBalance(user.UserId) ;
        }
        public async Task<double> Deposit(int userId, double amount)
        {
            int _accountId = await GetAccountIdByUserId(userId);
            UserDTO? user = await usersRepository.GetUserById(userId);
            if (user != null && _accountId != 0)
            {
                var account = user.Account.FirstOrDefault(x => x.AccountId == _accountId);
                if (account != null)
                {
                    account.Balance += amount;
                    return account.Balance;
                }
            }
            return (0.0);
        }

    }
}
