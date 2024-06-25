///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Models
{
    public class SimpleViewModelBase
    {
        protected SimpleViewModelBase(string title = "")
        {
            Title = title;
        }

        #region PUBLIC FACTORIES
        public static SimpleViewModelBase Default(string title = "")
        {
            var model = new SimpleViewModelBase(title);
            return model;
        }

        public static SimpleViewModelBase Default(AppSettings settings, string title = "")
        {
            var model = new SimpleViewModelBase(title) { Settings = settings, Title = title };
            if (title.IsNullOrWhitespace())
                model.Title = settings?.General.ApplicationName;
            return model;
        }
        #endregion


        /// <summary>
        /// Gets/sets the title of the view 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the settings of the application
        /// </summary>
        public AppSettings Settings { get; set; }

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
