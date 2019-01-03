using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Authority
{
    //public class Herbalife_HGDX_ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    //{
    //    public AppClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
    //        RoleManager<IdentityRole> roleManager,
    //        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    //    {
    //    }

    //    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    //    {
    //        var principal = await base.CreateAsync(user);
    //        ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
    //        new Claim(ClaimTypes.Role, "MyRole")
    //    });

    //        return principal;
    //    }
    //}
}
