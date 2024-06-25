///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//





namespace Salotto.App.Common.Settings
{
    public class RunSettings
    {
        public RunSettings()
        {
            DevMode = true;
            EnableLogging = true;
        }

        public bool DevMode { get; set; }
        public bool EnableLogging { get; set; }
    }
}