using apibase.Models;
using apibase.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
namespace apibase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService; // сервис для работы с базой данных

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // проверка пользователя в базе данных
            var user = await _userService.AuthenticateAsync(request.Login, request.Password);

            if (user == null)
                return Unauthorized("Неверное имя пользователя или пароль");

            // возвращаем данные пользователя

            return Ok(new
            {
                Userid = user.UserId,
                Username = user.Username,
                Userlogin = user.Login,
                Userphone = user.Phone
                
            });
        }
    }
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
    }
}
