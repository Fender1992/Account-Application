using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO_s
{
    public class UserDTO 
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AccountDTO> Account { get; set; }
        public bool Success { get; set; }
        public RolesDTO Role { get; set; }
    }
}
