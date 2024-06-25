///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System.Collections.Generic;
using Salotto.Resources;

namespace Salotto.Infrastructure.Services.Authentication
{
    public class AuthenticationChain
    {
        private readonly IList<IAuthenticationProvider> _authenticationChain;

        public AuthenticationChain()
        {
            _authenticationChain = new List<IAuthenticationProvider>();
        }

        /// <summary>
        /// Populates the list of authentication providers
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public AuthenticationChain Add(IAuthenticationProvider provider)
        {
            if (provider != null)
                _authenticationChain.Add(provider);

            return this;
        }

        /// <summary>
        /// Evaluates listed provider against provided credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticationResponse Evaluate(string email, string password)
        {
            foreach (var provider in _authenticationChain)
            {
                var response = provider.ValidateCredentials(email, password);
                if (response.Success || response.ShouldBreak)
                    return response;
            }

            return AuthenticationResponse.Fail()
                .AddMessage(AppMessages.Err_InvalidCredentials);
        }
    }
}
