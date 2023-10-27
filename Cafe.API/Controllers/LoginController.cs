#nullable disable
#pragma warning disable IDE0059 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cafe.Data.Models;
using Cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace Bugshield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EspressoEcstasyContext _context;
        public LoginController(EspressoEcstasyContext context)
        {
            _context = context;
        }
        /// <summary>
        /// To  Login  and send Response to Client side
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [Route("Users")]
        [HttpPost]
        public IActionResult EmployeeLogin(LoginModel loginModel)
        {

            var user = _context.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    Status = "Invalid",
                    Message = "Invalid User.",
                    Email = "Invalid",
                    Username = "Invalid",
                    Phone = "Invalid",
                    Roleid = 0,
                    Token = "Invalid",
                    Userid = 0,
                    Password = "Invalid"
                });
            }
            else
            {
                IActionResult tokenResult = GetToken(user);

                if (tokenResult is OkObjectResult okObjectResult)
                {
                    string jwtToken = okObjectResult.Value?.ToString();
                    var response = new Response
                    {
                        Status = "Success",
                        Message = "Login Successfully",
                        Email = user.Email,
                        Username = user.Uname,
                        Phone = user.Phone,
                        Roleid = user.Roleid ?? 3,
                        Token = jwtToken,
                        Userid = user.Userid,
                        Password = user.Password,
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Failed to generate token.");
                }
            }
        }
        /// <summary>
        /// To generate Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("gettoken")]
        public IActionResult GetToken(User user)
        {
            int? roleid = user.Roleid;

            var userRole = "no role";

            if (roleid == 1)
            {
                userRole = "Admin";
            }
            else if (roleid == 2)
            {
                userRole = "Users";
            }
            else if (roleid == null)
            {
                return BadRequest("User does not have a role.");
            }
            else
            {
                return BadRequest("User does not have a role.");
            }

            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr85UcOj69Kqw5R2Nmf4FWs03Hdx";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRole),
            };

            var token = new JwtSecurityToken(
                issuer: "JWTAuthenticationServer",
                audience: "JWTServicePostmanClient",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }

    }
}





