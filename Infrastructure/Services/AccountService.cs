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
        //Task<Tuple<UserDTO, string>> CreateAccount(string username, string password);
        Task<double> GetBalance(int userId);
        Task<double> WithdrawService(int userId, double amount);
        Task<double> DepositService(int userId, double amount);
        Task<Tuple<UserDTO, string>> DeleteAccount(int accountId);
        bool ValidateCredentials(string username, string password);
    }
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private IUsersRepository _userRepository;
        //public async Task<AccountDTO> CreateAccount(AccountDTO account)
        //{
        //    //await _accountRepository.Cre.AddAsync(account);
        //}
        public AccountService(IAccountRepository accountRepository, IUsersRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<double> GetBalance(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new ArgumentException("User ID must be greater than zero.");
            }
            else
            {
                double balance = await _accountRepository.GetBalance(userId);
                return balance;
            }

        }

        public async Task<double> WithdrawService(int userId, double amount)
        {
            var user = _userRepository.GetUserById(userId);
            double balance = await _accountRepository.GetBalance(userId);
            if (user != null && (amount > 0 && amount < balance))
            {
                balance = await _accountRepository.Withdraw(userId, amount);
                return balance;
            }
            else
            {
                throw new ArgumentException("User ID must be greater than zero.");
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<double> DepositService(int userId, double amount)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null && amount > 0)
            {
                double balance = await _accountRepository.Deposit(userId, amount);
                return balance;
            }
            else
            {
                throw new ArgumentException("User ID must be greater than zero.");
            }
        }

        public async Task<Tuple<UserDTO, string>> DeleteAccount(int userId)
        {
            UserDTO? user = await _userRepository.GetUserById(userId); // Use nullable type
            if (user != null) // Check for null explicitly
            {
                var accountId = await _accountRepository.GetAccountIdByUserId(userId);
                if (accountId != 0)
                {
                    await _accountRepository.DeleteAccount(accountId);
                    return new Tuple<UserDTO, string>(user, "Account deleted successfully.");
                }
                else
                {
                    throw new ArgumentException("Account ID must be greater than zero.");
                }
            }
            else
            {
                throw new ArgumentException("User ID must be greater than zero.");
            }
        }
    }
}
