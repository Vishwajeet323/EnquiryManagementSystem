using Enquiry.Domain.Entities.Login;
using Enquiry.Domain.Interfaces.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.Login
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        public string CreateJwtToken(User userInfo)
        {
            Claim[] claims = new Claim[]
                          {
                                 new Claim("UserId",userInfo.UserId.ToString()),
                                 new Claim("RoleId",userInfo.RoleId.ToString()),
                                 new Claim("UserName",userInfo.UserName),
                                 new Claim("FirstName",userInfo.FirstName),
                                 new Claim("Email", userInfo.Email),
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
