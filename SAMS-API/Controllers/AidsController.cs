using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AidsController : ControllerBase
    {
        private readonly StudentsAidsService aidsService;
        private ITemplateService _templateService;

        public AidsController(StudentsAidsService aidsService, ITemplateService templateService)
        {
            this.aidsService = aidsService;
            this._templateService = templateService;
        }

        [HttpGet()]
        [EnableCors("NotLogged")]
        public IActionResult Get()
        {
            var id = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }  
            var aids = aidsService.Get(Guid.Parse(id));
            if (aids !=null ) return Ok(aids);
            return BadRequest();
        }

        [HttpGet("GetAll")]
        [EnableCors("NotLogged")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            List<StudentAid> aids = aidsService.Get();
            if (aids != null) return Ok(aids);
            return BadRequest();
        }

        [HttpPost("add")]
        [EnableCors("NotLogged")]
        public IActionResult Add(StudentAid aid)
        {
            var id = "";
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            if (aidsService.AddAid(aid, Guid.Parse(id))) return Ok();
            return BadRequest();
        }

        [HttpGet("print")]
        [EnableCors("NotLogged")]
        public async Task<IActionResult> Print(Guid id)
        {
            var studentAids = aidsService.GetById(id);
            var firstStudentAids = studentAids.FirstOrDefault();
            var model = new Raport();

            if (firstStudentAids != null)
            {
                model.StartTime = firstStudentAids.DateTime;
                model.Content = firstStudentAids.Comment;
                model.Subject = firstStudentAids.Subject;
            }

            var html = await _templateService.RenderAsync("Templates/Raport", model);

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions()
            {
                Headless = true,
                ExecutablePath = Extensions.PuppeteerExtensions.ExecutablePath,
            });

            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(html);
            var pdfContent = await page.PdfStreamAsync(new PdfOptions()
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
            });


            return File(pdfContent, "application/pdf", "raport.pdf");
        }
    }
}
