///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Globalization;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Common.Settings
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            MinPasswordLength = 6;
            ResetPasswordLifetimeSecs = 300;    // 5 min
        }

        private static DateTime? _forcedDate = null;

        /// <summary>
        /// Project nme
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Application primary name
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Application description
        /// </summary>
        public string ApplicationDescription { get; set; }

        /// <summary>
        /// Indicates the copyright info
        /// </summary>
        public CopyrightSettings Copyright { get; set; }

        /// <summary>
        /// Indicates the version of the application 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Indicates the build display information 
        /// </summary>
        public string BuildInfo { get; set; }

        /// <summary>
        /// Default language/culture to use comprehensively when not otherwise set
        /// EX: en-US
        /// </summary>
        public string DefaultCulture { get; set; }

        /// <summary>
        /// Contact email to get in touch with the peopple behind the software
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// File system path to the Templates content root
        /// </summary>
        public string TemplateRoot { get; set; }

        /// <summary>
        /// Minimum length allowed for passwords
        /// </summary>
        public int MinPasswordLength { get; set; }

        /// <summary>
        /// Seconds of lifetime for the password reset token
        /// </summary>
        public int ResetPasswordLifetimeSecs { get; set; }

        /// <summary>
        /// If not null, indicates the date to set in the system (for test purposes, if time is involved it's meant to be UTC)
        /// </summary>
        private string _forcedDateAsDdMmYy = "";
        public string ForcedDateAsDdMmYy
        {
            get => _forcedDateAsDdMmYy;
            set
            {
                _forcedDateAsDdMmYy = value;
                if (!_forcedDateAsDdMmYy.IsNullOrWhitespace())
                {
                    if (DateTime.TryParseExact(_forcedDateAsDdMmYy, "dd/MM/yy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
                    {
                        _forcedDate = dateTime;
                    }
                }
            }
        }

        /// <summary>
        /// Current (logical) date
        /// </summary>
        /// <returns></returns>
        public DateTime SystemDate()
        {
            return _forcedDate.GetValueOrDefault(DateTime.UtcNow.Date);
        }
    }
}