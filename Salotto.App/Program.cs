///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Salotto.App
{
    /// <summary>
    /// Main class of the web-app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Root entry point in the web app
        /// </summary>
        /// <param name="args">CLI arguments (if any)</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Helper method designed for web-app initialization (ASP.NET Core default settings)
        /// </summary>
        /// <param name="args">CLI arguments (if any)</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
