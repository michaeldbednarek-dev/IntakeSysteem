using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using IntakeSysteemBack.Models;

namespace IntakeSysteemBack.Controllers
{

    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpPost("test")]
        public IActionResult idk()
        {
            //repeat 6 times
            
            return Unauthorized();
        }

        [HttpPost("login")]
        public IActionResult Login()//[FromBody] Login user)
        {
            try
            {
                JWT jwt = new JWT();
                return Ok(new AuthenticatedResponse { Token = jwt.Generate() });
            }
            catch
            {
                return Unauthorized();
            }                         
        }
    }
}
