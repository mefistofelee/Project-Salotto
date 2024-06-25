///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//





namespace Salotto.App.Common.Security
{
    /// <summary>
    /// Facade for the names of custom claims to be saved in the app cookie
    /// </summary>
    public class TmsClaimTypes
    {
        /// <summary>
        /// Claim name to store the app name in the auth cookie
        /// </summary>
        public static string Name => "TMS";

        /// <summary>
        /// Claim name to store the controller name in the auth cookie (IF NECESSARY)
        /// </summary>
        public static string Controller => "controller";

        /// <summary>
        /// Claim name to store the user company name in the auth cookie
        /// </summary>
        public static string CorporateName => "company";


        /// <summary>
        /// Claim name to store the user ID in the auth cookie
        /// </summary>
        public static string UserId => "userid";

        public static string Permissions => "permissions";


        // Current Run
        public static string CurrentRunId => "currentrunid";
        public static string CurrentRunName => "currentrunname";
    }
}