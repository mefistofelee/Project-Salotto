///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System.Threading;
using System.Threading.Tasks;
using Salotto.App.Application.Account;
using Salotto.App.Common.Extensions;
using Salotto.App.Common.Settings;
using Salotto.App.Models.Login;
using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Youbiquitous.Martlet.Core.Types;
using Salotto.App.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Salotto.App.Common.Helpers;
using System;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [EnsureRole(SalottoRole.System, SalottoRole.Admin)]
    public class UserController : SalottoBaseController
    {
        private readonly UserService _service;
        private readonly IEmailSender _emailSender;

        public UserController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor, IEmailSender emailSender)
            : base(settings, loggerFactory, accessor)
        {
            _service = new UserService(settings);
            _emailSender = emailSender;
        }

        /// <summary>
        /// Logged profile page
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        [ActionName("profile")]
        [EnsureRole(SalottoRole.System, SalottoRole.Admin)]
        public IActionResult ViewProfile()
        {
            var id = CurrentUser().UserId;
            // Check it's the logged user; throw otherwise
            EnsureLoggedUser(id);
            var model = _service.GetUserViewModel(LoggedPermissions(), id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile(User user, IFormFile headshot, bool headshotIsDefined)
        {
            var response = _service.SaveProfile(user, headshot?.OpenReadStream(), headshotIsDefined, Signature());
            await HttpContext.UpdateCookie();
            return Json(response);
        }

        /// <summary>
        /// Change the password of logged users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/user/cp")]        
        public IActionResult ChangePasswordOfLoggedUser(PasswordChangeViewModel input)
        {
            // Validate data
            if (input == null || !input.IsValid())
                return Json(CommandResponse.Fail().AddMessage(AppMessages.Info_Failed));

            // Change password and record time of operation
            var response = _service.ChangePassword(input.UserId, input.OldPassword, input.NewPassword, Signature());

            Thread.Sleep(1000);
            return Json(response);
        }

        /// <summary>
        /// Page for CREATE or UPDATE user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/user/edit/{id?}")]
        [EnsureRole(SalottoRole.System)]
        public IActionResult Edit(long id = 0)
        {
            var model = _service.GetUserViewModel(LoggedPermissions(), id);
            return View(model);
        }

        /// <summary>
        /// Save a new user or edit
        /// </summary>
        /// <param name="user"></param>
        /// <param name="headshot"></param>
        /// <param name="headshotIsDefined"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(User user, string psw, IFormFile headshot, bool headshotIsDefined, bool sendEmail)
        {
            var response = _service.SaveUser(user, psw, headshot?.OpenReadStream(), headshotIsDefined, sendEmail, Signature());

            if (sendEmail)
            {
                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "NUOVO ACCOUNT - LOGICO", $"Ti � stato creato un account su LOGICO. <br> Puoi impostare una password dal seguente link. <br> <br> {ServerUrl}/login/setpassword/{response.Key}");
                }
                catch (Exception)
                {
                    response.AddMessage($"{response.Message} - {AppStrings.EmailNotSent}");
                }
            }


            return Json(response);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var response = _service.DeleteUser(id);
            return Json(response);
        }

        //[HttpGet]
        //[Route("/user/photo/{id}")]
        //public IActionResult Foto(long id)
        //{
        //    var foto = _service.PhotoById(id);
        //    if (foto.Item1 != null && !foto.Item2.IsNullOrWhitespace())
        //        return File(foto.Item1, foto.Item2 ?? string.Empty);
        //    return null;
        //}
    }
}