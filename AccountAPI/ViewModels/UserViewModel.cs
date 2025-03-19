using Application.DTO_s;

namespace AccountAPI.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
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
