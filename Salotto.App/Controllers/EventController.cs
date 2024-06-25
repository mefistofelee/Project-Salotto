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
using Salotto.App.Application;
using Salotto.DomainModel.Activity;
using Salotto.App.Common.Extensions;
using Salotto.App.Common.Security;
using Salotto.DomainModel.UserAccount;

namespace Salotto.App.Controllers
{
    public class EventController : SalottoBaseController
    {
        private readonly EventService _eventService;

        public EventController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _eventService = new(settings);
        }

        #region GET

        [HttpGet]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Edit(long id = 0)
        {
            var model = _eventService.GetEventViewModel(id, LoggedPermissions());
            return View(model);
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = _eventService.GetEventsViewModel(LoggedPermissions());
            return View(model);
        }

        #endregion

        #region POST

        [HttpPost]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Save(Event @event)
        {
            if (@event.Id == 0)
                @event.CreatedByUserId = User.Logged().UserId;

            var response = _eventService.Save(@event, Signature());
            return Json(response);
        }

        [HttpPost]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Delete(long id)
        {
            var response = _eventService.Delete(id, Signature());
            return Json(response);
        }

        #endregion
    }
}