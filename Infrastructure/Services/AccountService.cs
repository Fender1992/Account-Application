using Application.DTO_s;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAccountService
    {
        //Task<AccountDTO> CreateAccount(string username, string password);
        Task<double> GetBalance(int userId, int accountId);
        Task<double> Withdraw(int userId, int accountId, double amount);
        void DeleteAccount(string username);
        bool ValidateCredentials(string username, string password);
    }
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        //public async Task<AccountDTO> CreateAccount(AccountDTO account)
        //{
        //    //await _accountRepository.Cre.AddAsync(account);
        //}
        public void DeleteAccount(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetBalance(int userId, int accountId)
        {
            double balance = await _accountRepository.GetBalance(userId, accountId);
            return balance;
        }

        public Task<double> Withdraw(int userId, int accountId, double amount)
        {
            var userAccount = _accountRepository.GetAccountById(userId, accountId);
            if (userAccount != null)
            {
                userAccount.Balance -= amount;
                return Task.FromResult(userAccount.Balance);
            }
            return Task.FromResult(0.0);
        }

        public bool ValidateCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
