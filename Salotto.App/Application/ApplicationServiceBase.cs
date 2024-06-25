///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.App.Common.Settings;

namespace Salotto.App.Application
{
    public class ApplicationServiceBase
    {
        public ApplicationServiceBase(AppSettings settings)
        {
            Settings = settings;
        }

        public AppSettings Settings { get; }
    }
}