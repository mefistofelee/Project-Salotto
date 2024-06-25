///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Salotto.App.Common.Helpers;
using Salotto.App.Common.Mvc;
using Salotto.App.Common.Settings;
using Salotto.App.Common.Telemetry;
using Salotto.Infrastructure.Persistence;
using Salotto.Resources;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Youbiquitous.Martlet.Core.Types.Locale;
using Youbiquitous.Martlet.Mvc.Binders;
using Youbiquitous.Martlet.Mvc.Core;
using Youbiquitous.Martlet.Services.Email;
using Youbiquitous.Martlet.SignalR.Hubs;

namespace Salotto.App
{
    /// <summary>
    /// Bootstrap class injected in the program
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Ctor laying the ground for configuration
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            _environment = env;

            var settingsFileName = env.IsDevelopment()
                ? "app-settings-dev.json"
                : "app-settings.json";

            var dom = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(settingsFileName, optional: true)
                .AddEnvironmentVariables()
                .Build();
            _configuration = dom;
        }

        /// <summary>
        /// Adds core services to the list
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Clear out default loggers added by the default web host builder (program.cs)
            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddConfiguration(_configuration.GetSection("Logging"));
                if(_environment.IsDevelopment()) 
                {
                    config.AddDebug();
                    config.AddConsole();
                }
            });

            // Authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login/login");
                    options.Cookie.Name = AppSettings.AuthCookieName;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                });

            // Configuration
            var settings = new AppSettings();
            _configuration.Bind(settings);

            // DI
            services.AddSingleton(settings);
            services.AddHttpContextAccessor();
            services.AddScoped<TmsCookieAuthenticationEvent>();

            // Telemetry
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<ITelemetryInitializer, TmsTelemetryInitializer>();

            // MVC
            services.AddLocalization();
            services.AddControllersWithViews(options =>
                {
                    options.ModelBinderProviders.Insert(0, new StringToBooleanModelBinderProvider());
                })
                .AddMvcLocalization()
                .AddRazorRuntimeCompilation();

            // SignalR
            services.AddSignalR();

            // Email Sender - Gmail
            services.Configure<EmailSettings>(options =>
            {
                options.SmtpServer = settings.Secrets.EmailSettings.SmtpServer;
                options.SmtpPort = settings.Secrets.EmailSettings.SmtpPort;
                options.UserName = settings.Secrets.EmailSettings.UserName;
                options.Password = settings.Secrets.EmailSettings.Password;
            });
            services.AddTransient<IEmailSender, SmtpEmailSender>();
        }


        /// <summary>
        /// Configures core services
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="settings"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings settings)
        {
            var provider = new CookieRequestCultureProvider()
            {
                CookieName = AppSettings.CultureCookieName
            };

            // Localization (the system will figure out the initial language)
            var languages = _configuration.GetSection("languages").Get<string[]>();
            var supportedCultures = AppSettings.GetSupportedCultures(languages);
            var defaultCulture = supportedCultures.Any() ? supportedCultures[0] : new CultureInfo(settings.General.DefaultCulture);
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,      // Formatting numbers, dates, etc.
                SupportedUICultures = supportedCultures,    // UI strings that we have localized
            };
            options.RequestCultureProviders.Insert(0, provider);
            app.UseRequestLocalization(options);

            // Initializes the ENUM description localizer (only for localizable enums -- if any)
            LocalizedDescriptionAttribute.Initialize();
            LocalizedDescriptionAttribute.Map.Add("strings", AppStrings.ResourceManager);
            LocalizedDescriptionAttribute.Map.Add("messages", AppMessages.ResourceManager);
            LocalizedDescriptionAttribute.Map.Add("biz", AppBiz.ResourceManager);

            // Error handling (CHANGE THIS MANUALLY AS APPROPRIATE)
            //app.UseExceptionHandler("/app/error");
            app.UseDeveloperExceptionPage();
            app.Use(async (context, next) =>
            {
                await next();       // let it go
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/app/error";
                    await next();
                }
            });

            // Set up SQL database
            SalottoDatabase.ConnectionString = settings.Secrets.GetConnectionStringFor(UserSecretsSettings.Salotto);
            var db = new SalottoDatabase();
            db.Database.EnsureCreated();
            SalottoDatabase.Seed(db);

            // Set up MongoDB
            //SalottoMongoDb.ConnectionString = settings.Secrets.MongoDbConnectionString;
            //SalottoMongoDb.DatabaseName = settings.Secrets.MongoDbDatabaseName;

            // Other initial config steps
            settings.General.TemplateRoot = Path.Combine(env.ContentRootPath, "templates");

            app.UseCookiePolicy();
            app.UseAuthentication();

            if (env.IsProduction())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Security response headers
            app.Use(async (context, next) =>
            {
                if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
                    context.Response.Headers.Add("X-Frame-Options", "DENY");    // SAMEORIGIN if need to use frames ourselves
                if (!context.Response.Headers.ContainsKey("X-Xss-Protection"))
                    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
                    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // SignalR endpoint for monitoring remote operations (see Ybq.Martlet.SignalR ref)
                endpoints.MapHub<DefaultMonitorHub>(DefaultMonitorHub.Endpoint);
            });
        }
    }
}
