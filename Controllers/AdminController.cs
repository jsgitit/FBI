using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FBI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public AdminController()
        {

        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<string> Login([FromBody] User user)
        {
            // TODO: Authenticate Admin with Database
            // If not authenticated, return 401 Unauthorized
            // else continue with flow below

            var claims = new List<Claim>
            {
                new Claim("type", "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult GenerateBadge([FromBody] Agent Agent)
        {
            var Claims = new List<Claim>
            {
                 new Claim("type", "Agent"),
                 new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString())
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var Token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                Claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }

    }

}
