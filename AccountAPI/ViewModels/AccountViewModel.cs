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
        [Required(ErrorMessage = "Account type must be Savings or Checking.")]
        public string AccountType { get; set; }
       
    }
}
