using Abbott.Tips.Framework.Dependency;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public class JwtTokenGenerator : ISingletonDependency
    {
        private readonly BasicJwtOption _option;

        public JwtTokenGenerator(BasicJwtOption option)
        {
            _option = option;
        }

        public TokenValidationParameters ExportTokenParameters()
        {
            return new TokenValidationParameters()
            {
                IssuerSigningKey = _option.GenerateKey(),
                ValidAudience = _option.Audience,
                ValidIssuer = _option.Issuer,
                ValidateLifetime = true,
                RequireExpirationTime = true
            };
        }

        public string GenerateToken(string userName, IEnumerable<Claim> claims, DateTime expiratoin)
        {
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(userName));
            identity.AddClaims(claims);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {
                Issuer = _option.Issuer,
                Audience = _option.Audience,
                SigningCredentials = _option.GenerateCredentials(),
                Subject = identity,
                Expires = expiratoin
            });
            return token;
        }

        public (ClaimsPrincipal, AuthenticationProperties) GenerateAuthTicket(string userName, IEnumerable<Claim> claims, DateTime expiratoin)
        {
            var principal = new ClaimsPrincipal();
            var authProps = new AuthenticationProperties();
            var token = GenerateToken(userName, claims, expiratoin);
            authProps.StoreTokens(new[]
            {
                new AuthenticationToken
                    {Name = JwtBearerDefaults.AuthenticationScheme ,Value = token}
            });
            return (principal, authProps);
        }
    }
}
