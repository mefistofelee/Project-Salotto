///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Globalization;
using System.Text.Json;
using Salotto.App.Common.Extensions;
using Salotto.App.Common.Settings;
using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Mvc.Core;

namespace Salotto.App.Controllers
{
    public class SalottoBaseController : Controller
    {
        public SalottoBaseController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
        {
            Settings = settings;
            HttpConnection = accessor;
            Logger = loggerFactory.CreateLogger(settings.General.ApplicationName);
        }

        /// <summary>
        /// Current server 
        /// </summary>
        protected string ServerUrl => $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

        /// <summary>
        /// Current culture
        /// </summary>
        protected CultureInfo Culture => HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;

        /// <summary>
        /// Centralized logging reference 
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gain access to application settings
        /// </summary>
        protected AppSettings Settings { get; }

        /// <summary>
        /// Gain access to the currently logged account
        /// </summary>
        /// <returns></returns>
        protected User CurrentUser()
        {
            return User?.Logged();
        }

        /// <summary>
        /// Signature of the user to be saved in CRUD ops
        /// </summary>
        /// <returns></returns>
        protected string Signature()
        {
            return User?.Logged().Email;
        }

        protected string LoggedPermissions()
        {
            return User?.Logged().Role;
        }

        /// <summary>
        /// Gain access to HTTP connection info
        /// </summary>
        protected IHttpContextAccessor HttpConnection { get; }

        /// <summary>
        /// Check whether the current account has the given ID
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected bool IsCurrentUser(string email)
        {
            var user = CurrentUser();
            if (user == null)
                return false;
            if (user.Email.IsNullOrWhitespace())
                return false;
            return user.Email.EqualsAny(email);
        }

        /// <summary>
        /// Package up remote connection information
        /// </summary>
        /// <returns></returns>
        public RemoteCaller Connection()
        {
            return RemoteCaller.Build(HttpConnection);
        }

        /// <summary>
        /// Checks whether the currently logged user is the same for which the request is made 
        /// </summary>
        /// <param name="id"></param>
        protected void EnsureLoggedUser(long id)
        {
            if (User == null || id < 0)
                throw new UnauthorizedAccessException(AppMessages.Err_UnauthorizedOperation);

            if (User.Logged().UserId != id)
                throw new UnauthorizedAccessException(AppMessages.Err_UnauthorizedOperation);
        }

        /// <summary>
        /// Helper to return plain JSON with no property naming convention applied
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult JsonIgnoreCase(object data)
        {
            return Json(data, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
    }
}
