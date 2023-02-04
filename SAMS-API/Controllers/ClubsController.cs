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
    public class ClubsController : Controller
    {

        private readonly ScienceClubsService clubsService;

        public ClubsController(ScienceClubsService clubsService)
        {
            this.clubsService = clubsService;
        }

        [EnableCors("NotLogged")]
        [AllowAnonymous]
        [HttpGet("get")]
        public IActionResult GetClubs()
        {
            var list = clubsService.Get();
            return Ok(list);
        }

        [EnableCors("NotLogged")]
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult AddClubs(AddClub club)
        {
            var id = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            bool added = clubsService.AddClub(club, Guid.Parse(id));
            if (!added) return UnprocessableEntity("The club was not created");
            return Ok();
        }

        [EnableCors("NotLogged")]
        [HttpGet("leave/{clubid}")]
        public IActionResult Leave(string clubid)
        {
            var id = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            bool added = clubsService.Leave(Guid.Parse(id), Guid.Parse(clubid));
            if (!added) return BadRequest("Cant leave");
            return Ok("ok");
        }

        [EnableCors("NotLogged")]
        [HttpGet("join/{clubid}")]
        public IActionResult Join(string clubid)
        {
            var id = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            bool added = clubsService.Join(Guid.Parse(id), Guid.Parse(clubid));
            if (!added) return BadRequest("Error");
            return Ok("ok");
        }
    }
}
