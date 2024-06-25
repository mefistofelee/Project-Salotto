using Salotto.App.Common.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Salotto.App.Application;

namespace Salotto.App.Controllers
{
    public class CardController : SalottoBaseController
    {
        private readonly CardService _cardService;

        public CardController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _cardService = new(settings);
        }

        #region GET

        /// <summary>
        /// display card for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ByUser(long userId)
        {
            var model = _cardService.GetCardViewModel(userId, LoggedPermissions());
            return View(model); 
        }

        #endregion

        #region POST
        #endregion
    }
}