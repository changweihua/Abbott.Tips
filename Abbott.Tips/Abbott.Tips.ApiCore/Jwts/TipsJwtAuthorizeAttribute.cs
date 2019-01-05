using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Jwts
{
    public class TipsJwtAuthorizeAttribute : AuthorizeAttribute
    {
        public TipsJwtAuthorizeAttribute()
        {
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," +
                                    JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
