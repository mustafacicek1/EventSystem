using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Webapi_BitirmeProjesi.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private DateTime _accessTokenExpiration;
        private TokenOption _tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOption>();
        }

        public AccessToken CreateToken(string name,string mail, string role)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.Expiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _accessTokenExpiration,
                signingCredentials: signingCredentials,
                claims: SetClaims(name,mail,role)
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Expiration = _accessTokenExpiration,
                Token = token
            };
        }

        private IEnumerable<Claim> SetClaims(string name,string mail, string role)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, mail));
            claims.Add(new Claim(ClaimTypes.Role,role));
            claims.Add(new Claim(ClaimTypes.Name,name));

            return claims;
        }
    }
}
