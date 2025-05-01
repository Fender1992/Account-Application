using AccountAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public Task<ActionResult<ApiResponse<string>>> Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                return Task.FromResult<ActionResult<ApiResponse<string>>>(Ok(ApiResponse<string>.CreateResponse<string>(true, "Login successful", "token123")));
            }
            else
            {
                return Task.FromResult<ActionResult<ApiResponse<string>>>(BadRequest(ApiResponse<string>.CreateResponse<string>(false, "Invalid credentials")));
            }
        }
    }
}
