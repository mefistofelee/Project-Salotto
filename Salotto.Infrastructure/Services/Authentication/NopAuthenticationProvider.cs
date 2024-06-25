///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.Infrastructure.Services.Authentication
{
    /// <summary>
    /// Sample authentication provider just comparing email and password
    /// </summary>
    public class NopAuthenticationProvider : IAuthenticationProvider
    {
        public NopAuthenticationProvider()
        {
            Name = "NOP";
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string Name { get; }

        public AuthenticationResponse ValidateCredentials(string email, string password)
        {
            if (email.EqualsAny(password))
                return AuthenticationResponse.Ok()
                    .AddSignature(Name)
                    .AddEmail(email)
                    .AddPermissions("")
                    .AddDisplayName(email);

            return AuthenticationResponse.Fail()
                .AddMessage(AppMessages.Err_InvalidCredentials);
        }
    }
}
