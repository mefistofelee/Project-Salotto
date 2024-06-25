///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//



namespace Salotto.Infrastructure.Services.Authentication
{
    /// <summary>
    /// Overall interface for authentication providers trying to validate credentials
    /// </summary>
    public interface IAuthenticationProvider
    {
        public string Name { get; }
        AuthenticationResponse ValidateCredentials(string email, string password);
    }
}
