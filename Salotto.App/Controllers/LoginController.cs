///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Threading;
using System.Threading.Tasks;
using Salotto.App.Application.Account;
using Salotto.App.Common.Extensions;
using Salotto.App.Common.Helpers;
using Salotto.App.Common.Settings;
using Salotto.App.Models;
using Salotto.App.Models.Login;
using Salotto.DomainModel.UserAccount;
using Salotto.Infrastructure.Persistence.Repositories;
using Salotto.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Youbiquitous.Martlet.Core.Types;

namespace Salotto.App.Controllers
{
    /// <summary>
    /// Provides endpoints for the whole sign-in/sign-out process
    /// </summary>
    public class LoginController : SalottoBaseController
    {
        private readonly AuthService _service;
        private readonly IEmailSender _emailSender;

        public LoginController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor, IEmailSender emailSender)
            : base(settings, loggerFactory, accessor)
        {
            _service = new AuthService(Settings);
            _emailSender = emailSender;
        }

        /// <summary>
        /// Displays the login page shared by all users of the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("login")]
        public IActionResult ShowSignInPage(string returnUrl)
        {
            var model = new LoginViewModel { Settings = Settings, FormData = { ReturnUrl = returnUrl } };
            return View(model);
        }

        /// <summary>
        /// Validate provided credentials and logs the user in
        /// </summary>
        /// <param name="input">Values of the login input form</param>
        /// <returns>JSON command response</returns>
        [HttpPost]
        [ActionName("login")]
        public async Task<JsonResult> TrySignIn(LoginInput input)
        {
            //Thread.Sleep(5000);
            var response = _service.TryAuthenticate(input);
            if (response.Success)
            {
                await HttpContext.AuthenticateUser(response, input.RememberMe);
                return Json(CommandResponse.Ok().AddRedirectUrl("/home/index"));
            }

            return Json(CommandResponse.Fail().AddMessage(response.Message));
        }

        /// <summary>
        /// Sign users out of the application
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }

        [HttpGet]
        [Route("/login/setpassword/{token}")]
        public IActionResult SetPassword(string token)
        {
            var model = _service.GetSetPasswordViewModel(token);
            return View(model);
        }

        [HttpPost]
        public IActionResult SetPassword(long userId, string token, string psw1, string psw2)
        {
            var response = _service.SetPassword(userId, token, psw1, psw2); 
            return Json(response);
        }

        /// <summary>
        /// Present the page through which the user can request to recover a forgotten password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("recover")]
        public IActionResult DisplayForgotPasswordView()
        {
            var model = LandingViewModelBase.Default(Settings);
            return View(model);
        }

        /// <summary>
        /// Send the reset link via email for a forgot-password scenario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("recover")]
        public async Task<IActionResult> SendLinkForPassword(string email)
        {
            var user = _service.TrySendLinkForPasswordReset(email);
            if(user != null)
            {
                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "RESET PASSWORD - LOGICO", $"Hai richiesto il reset della password per LOGICO. <br> Puoi impostare una nuova password dal seguente link. <br> <br> {ServerUrl}/account/reset/{user.PasswordResetToken}");
                    return Json(CommandResponse.Ok().AddMessage(AppStrings.Text_LinkSent));
                }
                catch (Exception)
                {
                    return Json(CommandResponse.Fail().AddMessage(AppStrings.EmailNotSent));
                }
            }
            return Json(CommandResponse.Fail().AddMessage(AppMessages.Err_UserNotFound));
        }

        /// <summary>
        /// Present the page through which the user can reset his/her password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/account/reset/{token}")]
        [ActionName("reset")]
        public IActionResult DisplayResetPasswordView(string token)
        {
            var model = _service.GetResetPasswordViewModel(token);
            return View(model);
        }

        /// <summary>
        /// Resets the password
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("reset")]
        public IActionResult ResetPassword(long userId, string token, string psw1, string psw2)
        {
            var response = _service.TryResetPassword(userId, token, psw1, psw2);
            return Json(response);
        }
    }
}
