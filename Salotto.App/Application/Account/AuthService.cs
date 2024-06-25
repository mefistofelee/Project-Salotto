///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Salotto.App.Models.Login;
using Salotto.App.Models.UserAccount;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.Infrastructure.Services;
using Salotto.Infrastructure.Services.Authentication;
using Salotto.Resources;
using PoponGate.ExtensionMethods;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Services.Email;
using Youbiquitous.Martlet.Services.Security;

namespace Salotto.App.Application.Account
{
    public class AuthService : ApplicationServiceBase
    {
        private readonly IPasswordService _passwordService;
        private readonly IFileService _fileService;
        private readonly AuthenticationChain _chain;

        public AuthService(AppSettings settings) : base(settings)
        {
            _passwordService = new DefaultPasswordService();
            _fileService = new DefaultFileService(Settings.General.TemplateRoot);

            _chain = new AuthenticationChain()
                .Add(new SalottoAuthenticationProvider())
                .Add(new NopAuthenticationProvider());
        }

        /// <summary>
        /// Try to locate a matching regular account (whether member or admin) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AuthenticationResponse TryAuthenticate(LoginInput input)
        {
            return _chain.Evaluate(input.UserName, input.Password);
        }

        /// <summary>
        /// Try to locate a matching regular account (whether member or admin) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AuthenticationResponse TryAuthenticateInternal(LoginInput input)
        {
            var c = new SalottoAuthenticationProvider();
            return c.ValidateCredentialsInternal(input.UserName, null, skipPasswordCheck: true);
        }

        /// <summary>
        /// Enables password reset by sending a link
        /// </summary>
        /// <param name="email"></param>
        /// <param name="server"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public User TrySendLinkForPasswordReset(string email)
        {
            if (email.IsNullOrWhitespace())
                return null;

            var userRepository = new UserRepository();
            var user = userRepository.MarkForPasswordReset(email);

            return user;
            //if (user == null)
            //    return CommandResponse.Fail().AddMessage(AppMessages.Info_Failed);

            //// Prepare email with reset link
            //var template = "email_password_reset.txt";
            //var messageFormat = _fileService.Load(template, lang);
            //var resetLink = $"{server}/account/reset/{user.PasswordResetToken}";
            //var message = string.Format(messageFormat, resetLink);

            //// Prepare and send email
            //var output = EmailSender.Send(email, null, AppStrings.Label_PasswordReset, message);
            //return CommandResponse.Ok().AddMessage(output);
        }

        /// <summary>
        /// Reset the user password  
        /// </summary>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public CommandResponse TryResetPassword(long userId, string token, string psw1, string psw2)
        {
            if (psw1.IsNullOrEmpty() || psw2.IsNullOrEmpty() || psw1 != psw2)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);

            var userRepository = new UserRepository();
            return userRepository.ResetPasswordByToken(userId, token, psw1);
        }

        /// <summary>
        /// Proxy to the password token checker method
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CommandResponse CheckPasswordToken(string token)
        {
            var user = UserRepository.FindByPasswordToken(token);
            return user == null ? CommandResponse.Fail() : CommandResponse.Ok();
        }

        public SetPasswordViewModel GetSetPasswordViewModel(string token)
        {
            var user = UserRepository.FindBySetPasswordToken(token);
            var model = new SetPasswordViewModel(Settings)
            {
                User = user,
                HasError = user == null,
                ErrorMessage = user == null ? AppMessages.Err_UserNotFound : string.Empty,
            };
            return model;
        }

        public CommandResponse SetPassword(long userId, string token, string psw1, string psw2)
        {
            if (psw1.IsNullOrEmpty() || psw2.IsNullOrEmpty() || psw1 != psw2)
                return CommandResponse.Fail().AddMessage(AppMessages.Err_OperationFailed);

            return UserRepository.SetPassword(userId, token, psw1);
        }


        public SetPasswordViewModel GetResetPasswordViewModel(string token)
        {
            var user = UserRepository.FindByPasswordToken(token);
            var model = new SetPasswordViewModel(Settings)
            {
                User = user,
                HasError = user == null,
                ErrorMessage = user == null ? AppMessages.Err_UserNotFound : string.Empty,
            };
            return model;
        }
    }
}
