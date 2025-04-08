using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid TransactionId { get; set; }
        public Account AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal"
        public string Description { get; set; } // Optional description of the transaction
        public string Status { get; set; } // e.g., "Completed", "Pending", "Failed"
        public User User { get; set; } // Navigation property to the User entity
        public Account Account { get; set; } // Navigation property to the Account entity

    }
}
