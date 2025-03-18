using Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UsersAccounts
    {
        public List<UserDTO> users = new List<UserDTO>
        {
            new UserDTO
            {
                UserId = 1,
                UserName = "user1",
                Password = "password1",
                FirstName = "John",
                LastName = "Doe",
                Account = new List<AccountDTO>
                {
                    new AccountDTO
                    {
                        AccountId = 1,
                        AccountName = "Account 1",
                        Balance = 1000,
                        CurrencyCode = "USD",
                        CanEdit = true
                    },
                }
            },
            new UserDTO
            {
                UserId = 2,
                UserName = "user2",
                Password = "password2",
                FirstName = "Jane",
                LastName = "Doe",
                Account = new List<AccountDTO>
                {
                    new AccountDTO
                    {
                        AccountId = 3,
                        AccountName = "Account 3",
                        Balance = 3000,
                        CurrencyCode = "USD",
                        CanEdit = true
                    },
                    new AccountDTO
                    {
                        AccountId = 4,
                        AccountName = "Account 4",
                        Balance = 4000,
                        CurrencyCode = "USD",
                        CanEdit = false
                    }
                }
            },
            new UserDTO
            {
                UserId = 3,
                UserName = "user3",
                Password = "password3",
                FirstName = "John",
                LastName = "Smith",
                Account = new List<AccountDTO>
                {
                    new AccountDTO
                    {
                        AccountId = 6,
                        AccountName = "Account 5",
                        Balance = 5000,
                        CurrencyCode = "USD",
                        CanEdit = true
                    },
                }
            },
            new UserDTO
            {
                UserId = 4,
                UserName = "user4",
                Password = "password4",
                FirstName = "Jane",
                LastName = "Smith",
                Account = new List<AccountDTO>
                {
                    new AccountDTO
                    {
                        AccountId = 7,
                        AccountName = "Account 7",
                        Balance = 7000,
                        CurrencyCode = "USD",
                        CanEdit = true
                    },
                    new AccountDTO
                    {
                        AccountId = 8,
                        AccountName = "Account 8",
                        Balance = 8000,
                        CurrencyCode = "USD",
                        CanEdit = false
                    }
                }
            }
        };
    }
}
