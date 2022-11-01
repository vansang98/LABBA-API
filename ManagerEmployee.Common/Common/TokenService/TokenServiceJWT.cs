using ManagerEmployee.EfCore.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Common.Common.TokenService
{
    /// <summary>Token JWT</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public class TokenServiceJWT : ITokenServiceJWT
    {
        /// <summary>Đặt thời gian đăng ký Token</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private const double EXPIRY_DURATION_MINUTES = 5;

        /// <summary>Tạo chuỗi Token</summary>
        /// <param name="key">The key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public string BuildToken(string key, string issuer, Employee employee)
        {
            var claims = new[] {
            new Claim(ClaimTypes.Name, employee.Phonenumber),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        /// <summary>Determines whether [is token valid] [the specified key].</summary>
        /// <param name="key">The key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if [is token valid] [the specified key]; otherwise, <c>false</c>.</returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
