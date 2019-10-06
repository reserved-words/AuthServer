// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer.Data;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public IWebHostEnvironment Environment { get; }
        
        public Startup(IWebHostEnvironment environment, IConfiguration config)
        {
            _config = config;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services
                .AddTransient<IUserStore, UserStore>()
                .AddTransient<IProviderStore, ProviderStore>()
                .AddTransient<IDataFetcher, DataFetcher>();

            var builder = services.AddIdentityServer()
                .AddResourceStore<ResourceStore>()
                .AddClientStore<ClientStore>();

            if (Environment.EnvironmentName == "Development")
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                var certificateThumbprint = _config.GetSection("Settings").GetValue<string>("CertificateThumbprint");

                X509Certificate2 cert = null;
                using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
                {
                    certStore.Open(OpenFlags.ReadOnly);
                    var certCollection = certStore.Certificates.Find(
                        X509FindType.FindByThumbprint,
                        certificateThumbprint,
                        false);

                    if (certCollection.Count > 0)
                    {
                        cert = certCollection[0];
                    }

                    builder.AddSigningCredential(cert);
                }
            }

            var authBuilder = services.AddAuthentication()
                .AddOpenIdConnect("oidc", "OpenID Connect", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;
                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            var googleClientID = _config.GetSection("Settings").GetValue<string>("GoogleClientID");
            var googleClientSecret = _config.GetSection("Settings").GetValue<string>("GoogleClientSecret");
            if (!string.IsNullOrEmpty(googleClientID) && !string.IsNullOrEmpty(googleClientSecret))
            {
                authBuilder.AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = googleClientID;
                    options.ClientSecret = googleClientSecret;
                });
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}