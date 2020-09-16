
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAppController : ControllerBase
    {
        [Authorize]
        [HttpGet("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Логин: {HttpContext.User.Identity.Name}\r\n" +
                $"Роль: {HttpContext.User.FindFirstValue(ClaimTypes.Role)}");
        }

        //[Authorize(role = "admin")]
        //[Route("getrole")]
        //public IActionResult GetRole()
        //{
        //    return Ok("Ваша роль: администратор");
        //}
    }
}
