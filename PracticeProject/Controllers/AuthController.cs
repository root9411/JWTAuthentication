using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticeProject.Data;
using PracticeProject.Model;
using PracticeProject.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace PracticeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        private readonly DatabaseContext _context;
        public AuthController(IConfiguration configuration , DatabaseContext dbContext)
        {
            this.configuration = configuration;
            _context = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] User user)
        {

            IActionResult response = Unauthorized();

            var userDetail = _context.Customers.ToList();
            SharedService sharedService = new SharedService();
            
            if (userDetail != null)
            {
                foreach(var a in userDetail)
                {

                    if(user.ClientId == a.ClientId.ToString())
                    {
                        var decreptResponse = sharedService.DecryptText(a.ClientSecret, a.ClientId.ToString());
                        
                        
                        if(user.ClientId == a.ClientId.ToString() && user.Password == decreptResponse)
                        {
                            if (user != null)
                            {
                                
                                    var issuer = configuration["Jwt:Issuer"];
                                    var audience = configuration["Jwt:Audience"];
                                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                                    var signingCredentials = new SigningCredentials(
                                                            new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha512Signature
                                                        );

                                    var subject = new ClaimsIdentity(new[]
                                    {
                                        //new Claim(JwtRegisteredClaimNames.Sub, a.FirstName),
                                        //new Claim(JwtRegisteredClaimNames.Email, a.FirstName),
                                        new Claim("ClientId", a.ClientId.ToString()),
                                        new Claim("DatabaseName", "CRUDEF"),

                                        //new Claim(ClaimTypes.GivenName, user.UserName),
                                    });

                                    var expires = DateTime.UtcNow.AddMinutes(10);

                                    var tokenDescriptor = new SecurityTokenDescriptor
                                    {
                                        Subject = subject,
                                        Expires = expires,
                                        Issuer = issuer,
                                        Audience = audience,
                                        SigningCredentials = signingCredentials,
                                        
                                    };

                                    var tokenHandler = new JwtSecurityTokenHandler();
                                    var token = tokenHandler.CreateToken(tokenDescriptor);
                                    var jwtToken = tokenHandler.WriteToken(token);

                                    // Set Token



                                    return Ok(jwtToken);
                                
                            }
                        }
                    }



                    
                }    
            }

         
            
            return response;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            else
            {
                Guid ClientId = Guid.NewGuid();
                Guid Id = Guid.NewGuid();

                customer.ClientId = ClientId;
                customer.Id = Id;

                SharedService sharedService = new SharedService();

                var encrypt = sharedService.EncryptText(customer.ClientSecret, ClientId.ToString());
                customer.ClientSecret = encrypt;

                var result = await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return Ok(result.Entity);
            }

        }


    }
}
