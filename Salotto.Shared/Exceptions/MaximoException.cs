///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Collections.Generic;
using Salotto.Resources;

namespace Salotto.Shared.Exceptions
{
    /// <summary>
    /// Base exception class to use within the application
    /// </summary>
    public class MaximoException : Exception
    {
        public MaximoException() : base(DefaultMessage())
        {
           
            RecoveryLinks = new List<RecoveryLink>();
        }
        public MaximoException(string message) : base(message)
        {
            RecoveryLinks = new List<RecoveryLink>();
        }
        public MaximoException(Exception exception) : this(exception.Message)
        {
        }

        /// <summary>
        /// Collection of recovery links to show in the UI
        /// </summary>
        public List<RecoveryLink> RecoveryLinks { get; }

        /// <summary>
        /// Default message for the exception
        /// </summary>
        public static string DefaultMessage()
        {
            var msg = AppMessages.ResourceManager.GetString("Err_Generic");
            return msg ?? AppMessages.Err_Generic;
        }

        /// <summary>
        /// Add a new recovery link through its list of pieces of information
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="blank"></param>
        /// <returns></returns>
        public MaximoException AddRecoveryLink(string text, string url, bool blank = false)
        {
            RecoveryLinks.Add(new RecoveryLink(text, url, blank));
            return this;
        }

        /// <summary>
        /// Add a new recovery link as an object
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public MaximoException AddRecoveryLink(RecoveryLink link)
        {
            RecoveryLinks.Add(link);
            return this;
        }
    }

    /// <summary>
    /// Wrapper class for links to show in the recovery UI
    /// </summary>
    public class RecoveryLink
    {
        public RecoveryLink(string text, string url, bool blank = false)
        {
            Text = text;
            Url = url;
            Blank = blank;
        }

        public string Text { get; }
        public string Url { get; }
        public bool Blank { get; }
    }

}