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

namespace Salotto.App.Controllers
{
    public class PostController : SalottoBaseController
    {
        private readonly PostService _postService;

        public PostController(AppSettings settings, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
            : base(settings, loggerFactory, accessor)
        {
            _postService = new PostService(settings);
        }

        #region GET

        [HttpGet]
        public IActionResult Edit(long id = 0)
        {
            var model = _postService.GetPostViewModel(id, LoggedPermissions());
            return View(model);
        }

        [HttpGet]
        public IActionResult All(long userId = 0)
        {
            var model = _postService.GetPostsViewModel(userId, LoggedPermissions());
            return View(model);
        }

        #endregion

        #region POST

        [HttpPost]
        public IActionResult Save(Post post, IFormFile file)
        {
            var response = _postService.Save(post, file, User.Logged());
            return Json(response);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var response = _postService.Delete(id, Signature());
            return Json(response);
        }

        #endregion
    }
}