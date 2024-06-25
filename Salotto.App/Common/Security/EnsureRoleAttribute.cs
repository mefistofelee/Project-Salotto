///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Extensions;
using Salotto.Resources;
using Salotto.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Salotto.App.Common.Security
{
    public class EnsureRoleAttribute : ActionFilterAttribute
    {
        public EnsureRoleAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; set; }

        /// <summary>
        /// Check performed before the controller method is invoked
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Locked out? Some further code here
            //var loggedUser = filterContext.HttpContext.User.Logged();

            // Method can execute regardless of roles
            if (Roles.Length == 0)
                return;

            var shouldThrow = true;
            foreach (var expectedRole in Roles)
            {
                var hasMatchingRole = filterContext.HttpContext.User.IsInRole(expectedRole);
                if (hasMatchingRole)
                {
                    shouldThrow = false;
                    break;
                }
            }

            if (shouldThrow)
            {
                throw new InvalidRoleException(AppMessages.Err_UnauthorizedOperation);
            }
        }
    }
}