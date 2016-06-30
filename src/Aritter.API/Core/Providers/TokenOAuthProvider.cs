﻿using Aritter.Application.DTO.SecurityModule.Authentication;
using Aritter.Application.Seedwork.Services.SecurityModule;
using Aritter.Infra.Crosscutting.Exceptions;
using Aritter.Infra.IoC.Providers;
using Aritter.Infra.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aritter.API.Core.Providers
{
    public class TokenOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            await Task.Run(() =>
            {
                var authenticationAppService = InstanceProvider.Get<IAuthenticationAppService>();
                var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
                var username = newIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                var authorization = authenticationAppService.GetAuthorization(username);

                if (authorization == null)
                {
                    return;
                }

                var identity = GenerateUserIdentity(authorization, OAuthDefaults.AuthenticationType);
                var newTicket = new AuthenticationTicket(identity, context.Ticket.Properties);

                context.Validated(newTicket);
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                try
                {
                    var authenticationAppService = InstanceProvider.Get<IAuthenticationAppService>();
                    var authorization = authenticationAppService.Authenticate(context.UserName, context.Password);

                    if (authorization == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }

                    var identity = GenerateUserIdentity(authorization, OAuthDefaults.AuthenticationType);
                    var properties = GenerateUserProperties(authorization);

                    var ticket = new AuthenticationTicket(identity, properties);

                    context.Validated(ticket);
                }
                catch (ApplicationErrorException ex)
                {
                    context.SetError(ex.Message);
                }
                catch (Exception ex)
                {
                    context.SetError(ex.Message);
                }
            });
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return base.TokenEndpointResponse(context);
        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            await Task.Run(() =>
            {
                foreach (var property in context.Properties.Dictionary)
                {
                    if (!property.Key.StartsWith("."))
                    {
                        context.AdditionalResponseParameters.Add(property.Key, property.Value);
                    }
                }

                context.AdditionalResponseParameters.Add("expires", context.Properties.ExpiresUtc.GetValueOrDefault().LocalDateTime);
            });
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => { context.Validated(); });
        }

        private static AuthenticationProperties GenerateUserProperties(AuthenticationDto authentication)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>());

            return properties;
        }

        private static ClaimsIdentity GenerateUserIdentity(AuthenticationDto authorization, string authenticationType)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authorization.User.UserName),
                new Claim(ClaimTypes.NameIdentifier, authorization.User.Identity.ToString()),
                new Claim(ClaimTypes.GivenName, authorization.User.Name)
            };

            claims.AddRange(GetRoleClaims(authorization.Roles));
            claims.AddRange(GetPermissionClaims(authorization.Permissions));

            var identity = new ClaimsIdentity(claims, authenticationType);

            return identity;
        }

        private static IEnumerable<Claim> GetRoleClaims(ICollection<string> roles)
        {
            foreach (var claim in roles)
            {
                yield return new Claim(Claims.Role, claim);
            }
        }

        private static IEnumerable<Claim> GetPermissionClaims(ICollection<string> permissions)
        {
            foreach (var claim in permissions)
            {
                yield return new Claim(Claims.Permission, claim);
            }
        }
    }
}