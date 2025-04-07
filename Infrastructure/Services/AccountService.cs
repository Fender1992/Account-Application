using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAccountService
    {
        void CreateAccount(string username, string password);
        void DeleteAccount(string username);
        bool ValidateCredentials(string username, string password);
    }
    public class AccountService : IAccountService
    {
        public void CreateAccount(string username, string password)
        {
            throw new NotImplementedException();
        }
        public void DeleteAccount(string username)
        {
            throw new NotImplementedException();
        }
        public bool ValidateCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
