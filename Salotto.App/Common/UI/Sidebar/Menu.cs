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
    /// Top-level menu 
    /// </summary>
    public class Menu
    {
        public Menu(string header = "")
        {
            Header = header;
            Items = new List<MenuItem>();
        }

        public string Header { get; private set; } 
        public List<MenuItem> Items { get; private set; }

        public Menu Item(string label, string url, string faIcon = "", string css = "", string target = "")
        {
            var item = new MenuItem(label, url, faIcon, css, target);
            Items.Add(item);
            return this;
        }
        public Menu Item(string label, string url, string faIcon, string css = "", string subMenuHeader = "", params SubMenuItem[] subItems)
        {
            var item = new MenuItem(label, url, faIcon, css) {Menu = {Header = subMenuHeader}};
            item.Menu.Items.AddRange(subItems);
            Items.Add(item);
            return this;
        }
    }

    /// <summary>
    /// Item of a top-level menu
    /// </summary>
    public class MenuItem
    {
        public MenuItem()
        {
        }
        public MenuItem(string label, string url = "", string faIcon = "", string css = "", string target = "")
        {
            Icon = faIcon;
            Url = url;
            Label = label;
            Css = css;
            Target = target;
            Menu = new SubMenu();
        }

        public string Icon { get; }
        public string Label { get; }
        public string Url { get; }
        public string Css { get; }
        public string Target { get; }
        public SubMenu Menu { get; }

        public bool HasSubmenu()
        {
            return Menu.Items.Any();
        }

        public bool IsDivider()
        {
            return Label.IsNullOrWhitespace();
        }
    }

    /// <summary>
    /// Second-level menu (slightly simpler)
    /// </summary>
    public class SubMenu
    {
        public SubMenu()
        {
            Items = new List<SubMenuItem>();
        }

        public string Header { get; set; } 
        public List<SubMenuItem> Items { get; set; }
    }

    /// <summary>
    /// Item of a second-level menu
    /// </summary>
    public class SubMenuItem
    {
        public SubMenuItem(string label, string url, string css = "", string target = "") 
        {
            Url = url;
            Label = label;
            Css = css;
            Target = target;
        }

        public SubMenuItem(string header)
        {
            Header = header;
            IsHeader = true;
        }

        public SubMenuItem()
        {
            IsDivider = true;
        }

        public string Css { get; }
        public string Header { get; }
        public string Label { get; }
        public string Url { get; }
        public string Target { get; }
        public bool IsDivider { get; }
        public bool IsHeader { get; }
    }
}