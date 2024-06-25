///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Salotto.App.Common.UI.Sidebar;
using Salotto.DomainModel.Helpers;
using Salotto.Infrastructure.Persistence.Repositories;
using System.Collections.Generic;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Models
{
    public class MainViewModelBase
    {
        public MainViewModelBase(string permissions)
        {
            Title = "";
            ShowSearchBox = false;
        }
        public MainViewModelBase(string permissions, AppSettings settings)
        {
            Title = "";
            ShowSearchBox = false;
            Settings = settings;
        }
        public MainViewModelBase(string permissions, AppSettings settings, string title = "")
        {
            Title = title;
            ShowSearchBox = false;
            Settings = settings;
        }

        #region PUBLIC FACTORIES
        public static MainViewModelBase Default(string permissions, string title = "")
        {
            var model = new MainViewModelBase(permissions) { Title = title };
            return model;
        }

        public static MainViewModelBase Default(string permissions, AppSettings settings, string title = "")
        {
            var model = new MainViewModelBase(permissions, settings, title);
            if (title.IsNullOrWhitespace())
                model.Title = settings?.General.ApplicationName;
            return model;
        }
        #endregion

        /// <summary>
        /// Gets/sets the settings of the application
        /// </summary>
        public AppSettings Settings { get; set; }


        /// <summary>
        /// Gets/sets the title of the view in the browser
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Indicates whether the search box should be visible 
        /// </summary>
        public bool ShowSearchBox { get; set; }

        /// <summary>
        /// Gets/sets the title of the page in the rendering area 
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Content of the sidebar
        /// </summary>
        public Sidebar Sidebar { get; set; }

        public IList<Country> Countries { get; set; }

        /// <summary>
        /// Indicates whether the model is in a valid state
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
