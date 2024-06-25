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
    public class EditionController : SalottoBaseController
    {
        private readonly EditionService _editionService;

        public EditionController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _editionService = new(settings);
        }

        #region GET

        [HttpGet]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Edit(long id = 0, long eventId = 0)
        {
            var model = _editionService.GetEditionViewModel(id, eventId, LoggedPermissions());
            return View(model);
        }

        [HttpGet]
        public IActionResult All(long eventId = 0)
        {
            var model = _editionService.GetEditionsViewModel(eventId, User.Logged().UserId, LoggedPermissions());
            return View(model);
        }

        #endregion

        #region POST

        [HttpPost]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Save(Edition edition)
        {
            if (edition.Id == 0)
                edition.ApprovedByUserId = User.Logged().UserId;

            var response = _editionService.Save(edition, Signature());
            return Json(response);
        }

        [HttpPost]
        [EnsureRole(SalottoRole.Admin, SalottoRole.System)]
        public IActionResult Delete(long id)
        {
            var response = _editionService.Delete(id, Signature());
            return Json(response);
        }

        [HttpPost]
        public IActionResult Join(long id)
        {
            var response = _editionService.BindUserAndEdition(id, User.Logged().UserId);
            return Json(response);  
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            var response = _editionService.UnbindUserAndEdition(id, User.Logged().UserId);
            return Json(response);
        }

        #endregion
    }
}