///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.Resources;
using Youbiquitous.Martlet.Services.Security;

namespace Salotto.Infrastructure.Services.Authentication
{
    /// <summary>
    /// Sample authentication provider just comparing email and password
    /// </summary>
    public class SalottoAuthenticationProvider : IAuthenticationProvider
    {
        public SalottoAuthenticationProvider()
        {
            Name = "LOGICO";
        }

        /// <summary>
        /// Name of the provider
        /// </summary>
        public string Name { get; }

        public AuthenticationResponse ValidateCredentials(string email, string password)
        {
            return ValidateCredentialsInternal(email, password, skipPasswordCheck: false);
        }

        
        /// <summary>
        /// Authenticate w/o password check 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="skipPasswordCheck"></param>
        /// <returns></returns>
        public AuthenticationResponse ValidateCredentialsInternal(string email, string password, bool skipPasswordCheck)
        {
            // Try as a supervisor
            var response1 = FindMatchingUser(email, password, skipPasswordCheck);
            if (response1.Success)
                return response1;

            return AuthenticationResponse.Fail();
        }

        /// <summary>
        /// Try to authenticate as a CASPIO user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="skipPasswordCheck"></param>
        /// <returns></returns>
        private AuthenticationResponse FindMatchingUser(string email, string password, bool skipPasswordCheck = false)
        {
            var authorizedUserRepository = new UserRepository();
            var passwordService = new DefaultPasswordService();

            var candidate = authorizedUserRepository.FindByEmail(email);
            if (candidate == null || !passwordService.Validate(password, candidate.Password))
                return AuthenticationResponse.Fail()
                    .AddMessage(AppMessages.Err_InvalidCredentials);

            // What if the user is locked out?
            if (candidate.Locked)
                return AuthenticationResponse.Fail()
                    .AddMessage($"{AppMessages.Err_AccountLocked} [{candidate.LockedMessage}]")
                    .Break();

            return AuthenticationResponse.Ok()
                .AddSignature(Name)
                .AddId(candidate.UserId)
                .AddRole(candidate.Role)
                .AddEmail(candidate.Email)
                .AddPermissions(candidate.SerializedPermissions)
                .AddPhotoUrl(candidate.PhotoUrl)
                .AddDisplayName(candidate.ToString());
        }
    }
}
