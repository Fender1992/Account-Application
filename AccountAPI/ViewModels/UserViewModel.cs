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
        public List<AccountViewModel> AccountId { get; set; }
    }
}
