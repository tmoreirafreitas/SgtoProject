using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sgot.Domain.Entities;
using Sgot.Infra.CrossCutting.IoC;
using Sgot.Infra.Data.Context;
using Sgot.Service.AutoMapper;
using Sgot.Service.Core.Responses;
using System;
using System.Reflection;
using System.Text;

namespace Sgot.Application.Api
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        readonly string migrationsAssembly = "Sgot.Infra.Data";//typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add CORS  
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            #endregion         

            #region Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //Política de senha
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                //Bloqueio de usuário.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;

                //Configuração de validação do usuário.
                options.User.RequireUniqueEmail = true;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            #endregion

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            #region Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenConfigurations:Key"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(1800)
                    };
                });
            #endregion            

            #region Add Authorization
            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion

            #region Add IdentityServer
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });
            #endregion     

            #region Add AutoMapper  
            services.AddAutoMapper();

            // Registering Mappings automatically only works if the 
            // Automapper Profile classes are in ASP.NET project
            AutoMapperConfig.RegisterMappings();
            #endregion

            #region Add IdentityCore
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(migrationsAssembly)));

            services.AddMediatR(typeof(NativeInjectorBootStrapper).GetTypeInfo().Assembly);

            services.AddMvc()
               .AddJsonOptions(options =>
               options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            #endregion            

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseMvc();
        }

        #region Helpers
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
        private static bool IsAjaxRequest(HttpRequest request)
        {
            var query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        private static bool IsApiRequest(HttpRequest request)
        {
            return request.Path.StartsWithSegments(new PathString("/api"));
        }
        
        private async void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<SgotDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                var usermanager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var rolemanager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new string[] { "ADMIN", "Vendedor", "Usuario" };
                var claims = new[,]
                {
                    { "ADMIN", "AD001" },
                    { "ADMIN", "AD002" },
                    { "VEND", "VE001" },
                    { "VEND", "VE002" },
                    { "VEND", "VE003" },
                    { "CEO", "CE001" },
                    { "CEO", "CE002" }
                };

                foreach (var role in roles)
                {
                    var hasRole = await rolemanager.RoleExistsAsync(role);
                    if (!hasRole)
                    {
                        await rolemanager.CreateAsync(new IdentityRole(role));
                    }
                }

                var hasUserApp = await usermanager.FindByEmailAsync("SgotUserApp@sgot.com.br");
                if (hasUserApp == null)
                {
                    var userAdmin = new ApplicationUser()
                    {
                        UserName = "SgotUserApp",
                        Email = "SgotUserApp@sgot.com.br",
                    };
                    var newUserResult = await usermanager.CreateAsync(userAdmin, "Sg0T2018");
                    if (newUserResult.Succeeded)
                    {
                        await usermanager.AddToRoleAsync(userAdmin, "ADMIN");
                        await usermanager.AddClaimAsync(userAdmin, new System.Security.Claims.Claim("ADMIN", "AD001"));
                        await usermanager.AddClaimAsync(userAdmin, new System.Security.Claims.Claim("CEO", "CE001"));
                    }
                }
            }
        }
        #endregion
    }
}
