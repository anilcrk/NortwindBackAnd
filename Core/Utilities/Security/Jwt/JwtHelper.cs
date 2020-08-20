using Core.Entities.Concrete;
using Core.Extentios;
using Core.Utilities.Security.Encryptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Remotion.Linq.Parsing;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;}
        private TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;   //conf. dosyası vasıtasıyla token options bilgilerini okuyoruz.
            _tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>(); //herşey token options sınıfına atıyor
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//süre
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            try
            {
                var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
                var sigingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
                var jwt = CreateJwtSecurityToken(_tokenOptions, user, sigingCredentials, operationClaims);
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtSecurityTokenHandler.WriteToken(jwt);

                return new AccessToken
                {
                    Token = token,
                    Expiration = _accessTokenExpiration
                };
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,User user,SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user,operationClaims),
                signingCredentials: signingCredentials);

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoler(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }
    }
}
