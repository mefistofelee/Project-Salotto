///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.DomainModel.UserAccount;
using Salotto.Resources;
using System;
using System.Collections.Generic;

namespace Salotto.App.Common.UI.Sidebar
{
    /// <summary>
    /// List of methods to build dedicated and context-sensitive menus
    /// </summary>
    public class MenuBuilder
    {
        /// <summary>
        /// For test purposes
        /// </summary>
        /// <returns></returns>
        public static Menu Home()
        {
            return new Menu()
                .Item(AppMenu.Home, "/home/index", "fas fa-home", "", "");
        }

        public static Menu Users()
        {
            return new Menu()
                .Item(AppMenu.Users, "/system/users", "fas fa-users", "", "");
        }

        public static Menu Tutorial()
        {
            return new Menu()
                .Item(AppMenu.Tutorial, "/admin/index", "fas fa-list", "", "");
        }

        public static Menu System()
        {
            return new Menu()
                .Item(AppMenu.Users, "/system/users", "fas fa-users", "", "")
                .Item(AppMenu.Events, "/system/events", "fas fa-list", "", "")
                .Item(AppMenu.Editions, "/system/editions", "fas fa-list", "", "");
        }

        public static Menu Admin()
        {
            return new Menu()
               .Item(AppMenu.Events, "/event/all", "fa-solid fa-people-group", "", "")
               .Item(AppMenu.Editions, "/edition/all", "fa-solid fa-calendar-days", "", "");
        }

        public static Menu Standard(long userId)
        {
            return new Menu()
               .Item(AppMenu.Events, "/event/all", "fa-solid fa-people-group", "", "")
               .Item(AppMenu.Editions, "/edition/all", "fa-solid fa-calendar-days", "", "")
               .Item(AppMenu.MyPosts, $"/post/all?userId={userId}", "fa-solid fa-image", "", "")
               .Item(AppMenu.MyPosts, $"/card/byuser?userId={userId}", "fa-solid fa-badge", "", "");
        }
    }
}