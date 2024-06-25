///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//



using System.Collections.Generic;
using System.Linq;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Common.UI.Sidebar
{
    /// <summary>
    /// Root class describing the sidebar menu(s)
    /// </summary>
    public partial class Sidebar 
    {
        public Sidebar()
        {
            Menus = new List<Menu>();
        }

        /// <summary>
        /// Menu items in the sidebar
        /// </summary>
        public List<Menu> Menus { get; }

        /// <summary>
        /// Builds the sidebar menu 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Sidebar AddMenu(params Menu[] menu)
        {
            Menus.AddRange(menu);
            return this;
        }
    }
}