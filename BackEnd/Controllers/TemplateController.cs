using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.Logic;

namespace IntakeSysteemBack.Controllers
{
    [Route("api/Template")]
    [ApiController]
    public class TemplateController : Controller
    {
        private readonly TemplateLogic _templateLogic;
        private readonly JWT _jwt;

        public TemplateController(TemplateLogic templatelogic, JWT jwt)
        {
            _templateLogic = templatelogic;
            _jwt = jwt;
        }

        [HttpPost("MakeThisVerySpecific")]
        public IActionResult MakeThisVerySpecific()//[FromBody] Login user)
        {
            //try
            //{
                _templateLogic.createFolder("Parent", "FolderName");
                return Ok();
            //}
            //catch
            //{
              //  return Unauthorized();
            //}
        }
    }
}
