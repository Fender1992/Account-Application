using Application.DTO_s;
using System.ComponentModel.DataAnnotations;

namespace AccountAPI.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Account Id is required.")]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public string CurrencyCode { get; set; }
        public bool CanEdit { get; set; }
        public string AccountType { get; set; }

        public static AccountDTO ToDTO(AccountViewModel accountViewModel)
        {
            return new AccountDTO
            {
                AccountId = accountViewModel.AccountId,
                AccountName = accountViewModel.AccountName,
                Balance = accountViewModel.Balance,
                CurrencyCode = accountViewModel.CurrencyCode,
                CanEdit = accountViewModel.CanEdit,
                AccountType = accountViewModel.AccountType
            };
        }
        public static AccountViewModel ToViewModel(AccountDTO accountDTO)
        {
            return new AccountViewModel
            {
                AccountId = accountDTO.AccountId,
                AccountName = accountDTO.AccountName,
                Balance = accountDTO.Balance,
                CurrencyCode = accountDTO.CurrencyCode,
                CanEdit = accountDTO.CanEdit,
                AccountType = accountDTO.AccountType
            };
        }
    }
}
