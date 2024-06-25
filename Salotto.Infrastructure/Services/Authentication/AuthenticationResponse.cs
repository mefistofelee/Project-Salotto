///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.Infrastructure.Services.Authentication
{
    public class AuthenticationResponse
    {
        /// <summary>
        /// Helper method for failed authentication
        /// </summary>
        /// <returns></returns>
        public static AuthenticationResponse Fail()
        {
            return new AuthenticationResponse(false);
        }

        /// <summary>
        /// Helper method for successful authentication
        /// </summary>
        /// <returns></returns>
        public static AuthenticationResponse Ok()
        {
            return new AuthenticationResponse(true);
        }

        /// <summary>
        /// General-purpose constructor
        /// </summary>
        /// <param name="success"></param>
        /// <param name="provider"></param>
        /// <param name="role"></param>
        public AuthenticationResponse(bool success, string provider = null, string permissions = null)
        {
            Success = success;
            AuthenticatedBy = provider;
            Permissions = permissions;
        }

        /// <summary>
        /// Add the name of auth provider tto he response
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public AuthenticationResponse AddSignature(string provider)
        {
            if (!provider.IsNullOrWhitespace())
                AuthenticatedBy = provider;
            return this;
        }

        /// <summary>
        /// Add the expected role of the authorized user within Corinto 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public AuthenticationResponse AddPermissions(string permissions)
        {
            if (!permissions.IsNullOrWhitespace())
                Permissions = permissions;
            return this;
        }

        /// <summary>
        /// Add the expected email of the authorized user within Corinto 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AuthenticationResponse AddEmail(string email)
        {
            if (!email.IsNullOrWhitespace())
                Email = email;
            return this;
        }

        /// <summary>
        /// Add the expected display name of the account
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AuthenticationResponse AddDisplayName(string name)
        {
            if (!name.IsNullOrWhitespace())
                DisplayName = name;
            return this;
        }

        /// <summary>
        /// Add the URL where to get the photo of the account (if any)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public AuthenticationResponse AddPhotoUrl(string url)
        {
            PhotoUrl = url.IsNullOrWhitespace() ? "/images/avatar.png" : url;
            return this;
        }

        /// <summary>
        /// Add team ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AuthenticationResponse AddTeamId(long id)
        {
            TeamId = id;
            return this;
        }

        /// <summary>
        /// Add ID (if any)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AuthenticationResponse AddId(long id)
        {
            Id = $"{id}";
            return this;
        }

        /// <summary>
        /// Add custom information
        /// </summary>
        /// <param name="extra"></param>
        /// <returns></returns>
        public AuthenticationResponse AddExtra(string extra)
        {
            if (!extra.IsNullOrWhitespace())
                Extra = extra;
            return this;
        }

        /// <summary>
        /// Add a return message for the user
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AuthenticationResponse AddMessage(string text)
        {
            if (!text.IsNullOrWhitespace())
                Message = text.Trim().TrimEnd('-');
            return this;
        }

        public AuthenticationResponse AddRole(string role)
        {
            if (!role.IsNullOrWhitespace())
                Role = role;
            return this;
        }

        /// <summary>
        /// Even if authentication failed, the chain should stop here
        /// </summary>
        /// <returns></returns>
        public AuthenticationResponse Break()
        {
            ShouldBreak = true;
            return this;
        }


        /// <summary>
        /// Read properties
        /// </summary>
        public bool Success { get; }
        public string AuthenticatedBy { get; private set; }
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string Permissions { get; private set; }
        public string DisplayName { get; private set; }
        public long TeamId { get; private set; }
        public string PhotoUrl { get; private set; }
        public string Extra { get; private set; }
        public string Message { get; private set; }
        public bool ShouldBreak { get; private set; }

        public string Role { get; private set; }
    }
}