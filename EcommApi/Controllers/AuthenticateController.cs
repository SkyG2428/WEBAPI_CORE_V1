using EcommApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EcommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManage, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManage = userManage;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists= await _userManage.FindByNameAsync(model.UserName);
            if (userExists != null)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error Message", Message = "Username already Exists" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result=await _userManage.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error Message", Message = "User Created Failed" });


            }
            else
            {
                if(! await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _userManage.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    if(!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                        await _userManage.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        await _userManage.AddToRoleAsync(user, "User");
                    }
                }
            }

            return Ok(new Response { Status = "Success", Message = "User Created Successfully" });

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManage.FindByNameAsync(model.UserName);
            if (user != null && await _userManage.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManage.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                };
                foreach (var UserRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, UserRole));
                }



                var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var toekn = new JwtSecurityToken(

                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(toekn),
                }
                );
            }
                return Unauthorized();
            

        }

    }
}
