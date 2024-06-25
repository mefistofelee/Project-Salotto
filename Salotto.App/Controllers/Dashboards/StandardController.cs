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

namespace Salotto.App.Controllers.Dashboards
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [EnsureRole(SalottoRole.System, SalottoRole.Admin, SalottoRole.Standard)]
    public class StandardController : SalottoBaseController
    {
        private readonly StandardService _standardService;
        public StandardController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _standardService = new(settings);
        }

        #region GET

        [HttpGet]
        public IActionResult Index()
        {
            var model = _standardService.GetStandardIndexViewModel(LoggedPermissions());
            return View(model);
        }

        #endregion

        #region POST



        #endregion
    }
}