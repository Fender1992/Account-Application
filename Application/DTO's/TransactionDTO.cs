using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO_s
{
    public class TransactionDTO
    {
        public Guid TransactionId { get; set; }
        public AccountDTO AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal"
        public string Description { get; set; } // Optional description of the transaction
        public string Status { get; set; } // e.g., "Completed", "Pending", "Failed"
        public string Message { get; set; } // e.g., "Completed", "Pending", "Failed"
        public UserDTO User { get; set; } // Navigation property to the User entity
        public AccountDTO Account { get; set; } // Navigation property to the Account entity
    }
}
