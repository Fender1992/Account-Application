using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO_s
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public string CurrencyCode { get; set; }
        public bool CanEdit { get; set; }
        public UserDTO User { get; set; }
        public string AccountType { get; set; }
    }
}
