using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace elcubo9.api.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            foreach (var claim in context.User)
            {
                var claimType = string.Format("urn:facebook:{0}", claim.Key);
                string claimValue = claim.Value.ToString();
                if (!context.Identity.HasClaim(claimType, claimValue))
                    context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

            }

            return Task.FromResult<object>(null);
        }
    }
}