using Salotto.App.Common.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Salotto.App.Controllers
{
    public class AppController : SalottoBaseController
    {
        public AppController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
        }

        /// <summary>
        /// Sets the language cookie and returns to the requesting page
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("/lang/{id}")]
        public IActionResult Lang(
            [Bind(Prefix = "id")] string cultureName = "it-IT",
            [Bind(Prefix = "r")] string returnUrl = "/home/index")
        {
            var culture = new RequestCulture(cultureName);
            var cookie = CookieRequestCultureProvider.MakeCookieValue(culture);
            Response.Cookies.Append(
                AppSettings.CultureCookieName,
                cookie,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }
    }
}
