using API.Entities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [EnableCors("NotLogged")]
        public IActionResult Login(LoginUser user)
        {
            var result = usersService.Login(user);

            if (result.Value.GetType() != typeof(TokenResponse))
                return BadRequest(result.Value);

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [EnableCors("NotLogged")]
        public IActionResult Refresh(TokenResponse token)
        {
            var result = usersService.Refresh(token);

            if (result.Value.GetType() != typeof(TokenResponse))
                return Unauthorized(result);

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [EnableCors("NotLogged")]
        public IActionResult Register(RegisterUser user)
        {
            var result = usersService.Register(user);

            if (result.Key != "ok")
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpGet("details")]
        [EnableCors("NotLogged")]
        public IActionResult Details()
        {
            var id = "";
            if(HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            User user =  usersService.Profile(Guid.Parse(id));
            if(user == null) return NotFound();
            return Ok(user);
        }
    }
}
