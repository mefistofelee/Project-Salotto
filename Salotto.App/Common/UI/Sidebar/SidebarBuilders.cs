///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.DomainModel.UserAccount;
using System;
using System.Collections.Generic;

namespace Salotto.App.Common.UI.Sidebar
{
    /// <summary>
    /// List of methods to build dedicated and context-sensitive sidebars
    /// </summary>
    public class SidebarBuilder
    {
        public static Sidebar FromPermissions(string permissions, long userId)
        {
            var options = new List<Menu>();

            return permissions switch
            {
                SalottoRole.System => SystemSidebar(),
                SalottoRole.Admin => AdminSidebar(),
                SalottoRole.Standard => StandardSidebar(userId),
                _ => new Sidebar().AddMenu(options.ToArray())
            };
        }

        private static Sidebar StandardSidebar(long userId)
        {
            var options = new List<Menu>();
            options.Add(MenuBuilder.Standard(userId));
            return new Sidebar().AddMenu(options.ToArray());
        }

        private static Sidebar AdminSidebar()
        {
            var options = new List<Menu>();
            options.Add(MenuBuilder.Admin());
            return new Sidebar().AddMenu(options.ToArray());
        }

        private static Sidebar SystemSidebar()
        {
            var options = new List<Menu>();
            options.Add(MenuBuilder.System());
            return new Sidebar().AddMenu(options.ToArray());
        }

        public static Sidebar ForUser(User user)
        {
            return FromPermissions(user.Role, user.UserId);
        }
    }
}