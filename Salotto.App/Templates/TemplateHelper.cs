///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System.IO;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.App.Templates
{
    public class TemplateHelper
    {
        public const string DefaultLanguageIsoCode = "en";

        /// <summary>
        /// Return a localized name for a given template file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="iso"></param>
        /// <returns></returns>
        public static string LocalizedFileName(string file, string iso)
        {
            if (iso.IsNullOrWhitespace() || iso.EqualsAny(DefaultLanguageIsoCode))
                return file;

            return Path.ChangeExtension(file, $"{iso}.txt");
        }
    }
}