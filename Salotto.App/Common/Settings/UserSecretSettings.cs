///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//



namespace Salotto.App.Common.Settings
{
    public class UserSecretsSettings
    {
        public const string Live = "live";
        public const string Staging = "staging";
        public const string Local = "local";
        public const string Salotto = "Salotto";

        public UserSecretsSettings()
        {

        }

        /// <summary>
        /// TMS backend DB to target
        /// </summary>
        public string SalottoBackendMode { get; set; }

        /// <summary>
        /// Connection string to in-house DB 
        /// </summary>
        public string SalottoConnectionStringLocal { get; set; }

        /// <summary>
        /// Connection string to staging DB 
        /// </summary>
        public string SalottoConnectionStringStaging { get; set; }

        /// <summary>
        /// Connection string to live DB 
        /// </summary>
        public string SalottoConnectionStringLive { get; set; }


        public string MongoDbConnectionString {  get; set; }
        public string MongoDbDatabaseName { get; set; }

        public EmailSettings EmailSettings { get; set; }

        /// <summary>
        /// Return the appropriate connection string for the specified target/mode 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetConnectionStringFor(string target)
        {
            return target switch
            {
                Salotto => SalottoBackendMode switch
                {
                    Live => SalottoConnectionStringLive,
                    Staging => SalottoConnectionStringStaging,
                    _ => SalottoConnectionStringLocal
                },
                _ => ""
            };
        }

        /// <summary>
        /// Backend mode just for display purposes on the landing page
        /// </summary>
        /// <returns></returns>
        public string GetBackendModeForDisplay()
        {
            return SalottoBackendMode switch
            {
                Staging => Staging,
                Local => Local,
                _ => ""
            };
        }

        /// <summary>
        /// Backend mode just for display purposes on logged box dropdown of each page
        /// </summary>
        /// <returns></returns>
        public string GetDbMonikerForDisplay()
        {
            return SalottoBackendMode switch
            {
                Staging => "STAGING",
                Local => "LOCAL DB",
                _ => "LOGICO"
            };
        }

        /// <summary>
        /// Whether the backend working mode is set to LIVE
        /// </summary>
        /// <returns></returns>
        public bool IsLiveMode()
        {
            return SalottoBackendMode == Live;
        }

        /// <summary>
        /// Whether the backend working mode is set to STAGING
        /// </summary>
        /// <returns></returns>
        public bool IsBetaMode()
        {
            return SalottoBackendMode == Staging;
        }
    }
}