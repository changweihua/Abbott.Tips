using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public abstract class TipsJwtOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public bool EnableCookie { get; set; }
        /// <summary>
        /// 自定义 Cookie 选项，可空
        /// </summary>
        public Action<CookieAuthenticationOptions> CookieOptions { get; set; }
        /// <summary>
        /// 自定义 jwt 选项，可空 
        /// </summary>
        public Action<JwtBearerOptions> JwtOptions { get; set; }
        public abstract SecurityKey GenerateKey();
        public abstract SigningCredentials GenerateCredentials();
    }
}
