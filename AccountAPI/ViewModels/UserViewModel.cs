using Application.DTO_s;
using System.ComponentModel.DataAnnotations;

namespace AccountAPI.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        public bool Success { get; set; } = false;
        public List<AccountViewModel> Account { get; set; }

        public static UserDTO ToDTO(UserViewModel userViewModel)
        {
            return new UserDTO
            {
                UserName = userViewModel.UserName,
                Password = userViewModel.Password,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Success = userViewModel.Success,
                Account = userViewModel.Account.Select(account => new AccountDTO
                {
                    AccountId = account.AccountId,
                    AccountName = account.AccountName,
                    Balance = account.Balance,
                    CurrencyCode = account.CurrencyCode,
                    CanEdit = account.CanEdit
                }).ToList()
            };
        }
        public static UserViewModel ToViewModel(UserDTO userDTO)
        {
            return new UserViewModel
            {
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Success = userDTO.Success,
                Account = userDTO.Account.Select(account => new AccountViewModel
                {
                    AccountId = account.AccountId,
                    AccountName = account.AccountName,
                    Balance = account.Balance,
                    CurrencyCode = account.CurrencyCode,
                    CanEdit = account.CanEdit
                }).ToList()
            };
        }
    }
}
