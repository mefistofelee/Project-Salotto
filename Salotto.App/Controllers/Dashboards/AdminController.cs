///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Security;
using Salotto.App.Common.Settings;
using Salotto.App.Models;
using Salotto.DomainModel.UserAccount;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salotto.App.Application;
using Salotto.DomainModel.Activity;
using Salotto.App.Common.Extensions;

namespace Salotto.App.Controllers.Dashboards
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [EnsureRole(SalottoRole.System, SalottoRole.Admin)]
    public class AdminController : SalottoBaseController
    {
        private readonly AdminService _adminService;
        private readonly PostService _postService;
        public AdminController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _adminService = new(settings);
            _postService = new(settings);
        }

        #region GET

        /// <summary>
        /// Home page for ADMIN users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var model = _adminService.GetAdminIndexViewModel(LoggedPermissions(), PostStatus.Approved);
            return View(model);
        }

        /// <summary>
        /// List of pending posts waiting to be approved by admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PendingPosts()
        {
            var model = _adminService.GetAdminIndexViewModel(LoggedPermissions(), PostStatus.Pending);
            return View(model);
        }

        #endregion

        #region POST

        [HttpPost]
        public IActionResult ChangePostStatus(long postId, PostStatus status)
        {
            var response = _postService.ChangePostStatus(postId, status, User.Logged().UserId, Signature());
            return Json(response);
        }

        #endregion
    }
}