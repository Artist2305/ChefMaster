using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dinner.Models;
using Dinner.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Dinner
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {           
           // services.AddDbContextPool<AppDbContext>(
           //     options => options.UseSqlServer(_config.GetConnectionString("RecipeDBConnection")));

            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(_config.GetConnectionString("RecipeDBConnection")),
             ServiceLifetime.Transient); 

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(5));

            services.AddControllersWithViews(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                //    IConfigurationSection googleAuthNSection =
                 //       Configuration.GetSection("Authentication:Google");

                    options.ClientId = "187939269260-klq746s0o50iesm2gsmc495d6hrgg6e3.apps.googleusercontent.com";
                    options.ClientSecret = "ictWu0RbxQOTiqOPsoiiUpaS";
                })

                .AddFacebook(options =>
                { 
                    options.AppId = "1379488558919733";
                    options.AppSecret = "807adc4ee3f65319eabb983cad842c00";
                }); 


            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy",
                   policy => policy.RequireRole("Admin"));
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IRecipeRepository, SQLRecipeRepository>();
            services.AddScoped<IRateRepository, SQLRatingRepository>();

            services.AddScoped<IObserveRepository, SQLObserveRepository>();
            services.AddScoped<IFavouriteRepository, SQLFavouriteRepository>();
            services.AddScoped<ICommentRepository, SQLCommentRepository>();

            services.AddScoped<IReportRepository, SQLReportRepository>();

            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            services.AddSingleton<DataProtectionPurposeString>();

            //services.AddSignalR();

            var emailConfig = _config
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
               // app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
              //  endpoints.MapHub<MyHub>("/MyHub");
            });

        }
    }
}
