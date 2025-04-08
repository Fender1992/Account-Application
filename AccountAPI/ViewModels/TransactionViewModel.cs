using Domain.Entities;

namespace AccountAPI.ViewModels
{
    public class TransactionViewModel
    {
        public Guid TransactionId { get; set; }
        public AccountViewModel AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal"
        public string Description { get; set; } // Optional description of the transaction
        public string Status { get; set; } // e.g., "Completed", "Pending", "Failed"
        public UserViewModel User { get; set; } // Navigation property to the User entity
        public AccountViewModel Account { get; set; } // Navigation property to the Account entity
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
