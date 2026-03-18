using EmployeeManagement.IRepository;
using EmployeeManagement.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandlerRepository tokenHandlerRepository;

        public AuthController(IUserRepository _userRepository, ITokenHandlerRepository _tokenHandlerRepository)
        {
            userRepository = _userRepository;
            tokenHandlerRepository = _tokenHandlerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {  
            // Validate the incoming request


            // Check if user is authenticated
            // Check Username and password

            var user = await userRepository.AuthenticateAsync(loginDTO.Username, loginDTO.Password);

            if (user != null)
            {
                var token = await tokenHandlerRepository.CreateTokenAsync(user);
                return Ok(token);

                // Generate JWT Token
            }

            return BadRequest("Invalid Credentials");
        }
    }
}
