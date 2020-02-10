﻿using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TokenBasedAuthenticaiton.Filter
{
    public class AuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName=="admin" && context.Password=="admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("UserName", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "zahidul"));
                context.Validated(identity);
            }
            else if(context.UserName=="user" && context.Password=="user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("UserName", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "zahidul islam"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid grand error.", "password or userName is incorrect.");
            }
        }
    }
}