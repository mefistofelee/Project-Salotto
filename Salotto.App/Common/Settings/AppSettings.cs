///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System.Collections.Generic;
using System.Globalization;
using Youbiquitous.Martlet.Core.Extensions;
using Youbiquitous.Martlet.Services.Email;

namespace Salotto.App.Common.Settings
{
    public class AppSettings
    {
        public const string AuthCookieName = "TMS.Auth";
        public const string CultureCookieName = "TMS.Culture";
        public static readonly string AppName = "TMS";

        public AppSettings()
        {
            Languages = new List<string>();
            General = new GeneralSettings();
            Run = new RunSettings();
            Secrets = new UserSecretsSettings();
        }

        public GeneralSettings General { get; set; }
        public List<string> Languages { get; set; }
        public RunSettings Run { get; set; }
        public UserSecretsSettings Secrets { get; set; }

        /// <summary>
        /// Returns the list of cultures supported by the application
        /// </summary>
        /// <returns></returns>
        public IList<CultureInfo> SupportedCultures()
        {
            var list = new List<CultureInfo>();
            foreach (var language in Languages)
            {
                if (language.IsNullOrWhitespace())
                    continue;
                list.Add(new CultureInfo(language));
            }
            return list;
        }

        /// <summary>
        /// For the list of languages (ie, en-us), returns the list of  CultureInfo objects
        /// </summary>
        /// <param name="languages"></param>
        /// <returns></returns>
        public static IList<CultureInfo> GetSupportedCultures(string[] languages)
        {
            if (languages == null)
                return new List<CultureInfo>();

            var list = new List<CultureInfo>();
            foreach (var language in languages)
            {
                if (language.IsNullOrWhitespace())
                    continue;
                list.Add(new CultureInfo(language));
            }

            return list;
        }
    }
}