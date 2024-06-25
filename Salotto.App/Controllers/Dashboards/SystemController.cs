///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Application;
using Salotto.App.Application.Account;
using Salotto.App.Common.Security;
using Salotto.App.Common.Settings;
using Salotto.App.Models;
using Salotto.DomainModel.UserAccount;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salotto.DomainModel.Activity;

namespace Salotto.App.Controllers.Dashboards
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [EnsureRole(SalottoRole.System)]
    public class SystemController : SalottoBaseController
    {
        private readonly UserService _userService;
        private readonly SystemService _systemService;
        public SystemController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _userService = new(settings);
            _systemService = new(settings);
        }

        #region GET

        /// <summary>
        /// Home page for SYSTEM users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var model = _systemService.GetSystemIndexViewModel(LoggedPermissions(), PostStatus.Approved);
            return View(model);
        }

        [HttpGet]
        public IActionResult Users()
        {
            var model = _userService.GetUsersViewModel(LoggedPermissions());    
            return View(model);
        }

        #endregion

        #region POST



        #endregion
    }
}