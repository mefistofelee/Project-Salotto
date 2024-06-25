///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Salotto.App.Controllers
{
    public class HomeController : SalottoBaseController
    {
        public HomeController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
        }

        /// <summary>
        /// Home page with the general dashboard
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Index", CurrentUser().Role);
        }
    }
}