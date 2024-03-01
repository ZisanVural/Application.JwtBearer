using Application.JwtBearer.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.JwtBearer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        private IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {
          Token token =TokenHandler.CreateToken(_configuration);
          return Ok(token);
        }
        
        [Authorize]

        [HttpGet("Index")]
        public string Index(string name)
        {
            return name;
        }
    }
}
