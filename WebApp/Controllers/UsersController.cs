using Microsoft.AspNetCore.Mvc;
using Models.secr;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.dto;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Email или пароль не найдены!" });

                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("registration/{password}")]
        public async Task<IActionResult> Registration(AuthenticateResponse model, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _userService.Registration(model, password);

                    if (response == null)
                        return BadRequest(new { message = "В регистрации отказано!" });

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    BadRequest(new { message = "В регистрации отказано!", ex.Message });
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
    }
}
