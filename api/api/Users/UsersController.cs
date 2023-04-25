using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace pillar.User
{
    [ApiController]
    [Route("users")]
    public class UsersController : Controller
    {
        [HttpPost("")]
        [Produces("application/json", Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (user is null) return null;
            var hashedpass = SecurePasswordHasher.Hash(user.Password);
            var response = Repository.Users.Add(user, hashedpass);
            
            return Ok(response);
        }

        [HttpGet("")]
        [Produces("application/json", Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AllUsers()
        { 
            var response = Repository.Users.RetrieveAll();
            return Ok(response);
        }

        [HttpGet("login/{userName}/{password}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> VerifyUser(
            [FromRoute] string userName,
            [FromRoute] string password)
        {
            if (userName is null || password is null) return BadRequest("Missing Username or Password.");
            
            var response = Repository.Users.Verify(userName.ToLower());
            if (response == null)
            {
                Console.Write("Not a valid Username. Please try again.");
                return StatusCode(403);
            }
            
            var result = SecurePasswordHasher.Verify(password, response["hashedPass"]);
            if (result == false)
            {
                Console.Write("Not a valid Password. Please try again.");
                return StatusCode(403);
            }
            
            Console.Write("Username/ password correct. Logging in.");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("p!ll@r-t!ck3t-$y$t3m"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials
            );
            
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new
            {
                bearer = token,
                userId = response["userId"],
                admin = response["admin"]
            });
        }
        
        [HttpGet("{userId}")]
        [Produces("application/json", Type = typeof(User))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RetrieveUser(
            [FromRoute] int userId)
        {
            if (userId == 0) return null;
            var response = Repository.Users.Retrieve(userId);
            return Ok(response);
        }
        
        [HttpDelete("{userId}"), Authorize]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (userId == 0) return null;
            Repository.Users.Delete(userId);
            
            return StatusCode(200);
        }
    }
}