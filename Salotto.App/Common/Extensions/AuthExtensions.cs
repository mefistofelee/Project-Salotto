///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Salotto.App.Common.Security;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Common.Extensions
{
    public static class AuthExtensions
    {
        public static User Logged(this ClaimsPrincipal user)
        {
            var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var photoUrl = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Uri)?.Value;
            var userId = user.Claims.FirstOrDefault(c => c.Type == TmsClaimTypes.UserId)?.Value;
            var permissions = user.Claims.FirstOrDefault(c => c.Type == TmsClaimTypes.Permissions)?.Value;

            // Can't make the query here as not all users come from the local DB
            var persona = new User()
            {
                UserId = userId.ToLong(),
                LastName = name,
                PhotoUrl = photoUrl,
                SerializedPermissions = permissions,
                Email = email,
                Role = role
            };

            return persona;
        }

        /// <summary>
        /// Creates the auth cookie for a regular user of the application
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public static async Task AuthenticateUser(this HttpContext context, AuthenticationResponse response, bool rememberMe)
        {
            await CreateTmsCookieFromAuth(context, response, rememberMe);
        }

        /// <summary>
        /// Refresh the cookie after changes to the profile
        /// </summary>
        /// <param name="context"></param>
        /// <param name="persona"></param>
        /// <returns></returns>
        public static async Task UpdateCookie(this HttpContext context)
        {
            var loggedId = context.User.Logged().UserId;
            var user = new UserRepository().FindById(loggedId);
            await CreateTmsCookieInternal(context, user, false);
        }

        /// <summary>
        /// Convert auth info into object and create related cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private static async Task CreateTmsCookieFromAuth(HttpContext context, AuthenticationResponse response, bool rememberMe)
        {
            var p = new User()
            {
                Email = response.Email,
                SerializedPermissions = response.Permissions.IsNullOrWhitespace() ? "*" : response.Permissions,
                FirstName = response.DisplayName,
                LastName = "",
                UserId = response.Id.ToLong(),
                Role = response.Role
            };

            await CreateTmsCookieInternal(context, p, rememberMe);
        }

        /// <summary>
        /// Actual code to create the app cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="persona"></param>
        /// <param name="authProvider"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private static async Task CreateTmsCookieInternal(HttpContext context, User user, bool rememberMe)
        {
            // Create the authentication cookie
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.ToString()),
                new Claim(TmsClaimTypes.Permissions, user.SerializedPermissions ?? "*"),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Uri, user.PhotoUrl ?? ""),
                new Claim(TmsClaimTypes.UserId, $"{user.UserId}")
            };
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            try
            {
                await context.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = rememberMe
                    });
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}