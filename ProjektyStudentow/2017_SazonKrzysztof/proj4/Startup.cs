using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using proj4.Data;
using proj4.Models;
using proj4.Services;
using Microsoft.AspNetCore.Authorization;
using proj4.Authorization;
using Microsoft.Extensions.Logging;

namespace proj4 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<MusicContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            #region state
            services.AddDistributedMemoryCache();
            //services.AddDistributedRedisCache(options => {
            //    options.Configuration = "127.0.0.1";
            //    options.InstanceName = "master";
            //});
            services.AddSession(options => {
                options.Cookie.Name = ".sazonoweciastko";
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });
            #endregion
            services.AddMvc();
            services.AddScoped<IAuthorizationHandler
                , PersonalDataIsOwnerAuthorizationHandler>();

            services.AddSingleton<IAuthorizationHandler
                , BandManagerAuthorizationHandler>();

            services.AddSingleton<AppData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            , IServiceProvider serviceProvider) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "add_band",
                    template: "add/",
                    defaults: new { controller = "Bands", action = "Create" });
            });

            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider) {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "BandManager" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists) {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var poweruser = new ApplicationUser {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"],
            };

            string userPWD = Configuration.GetSection("UserSettings")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (_user == null) {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded) {
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                    await UserManager.AddToRoleAsync(poweruser, "BandManager");
                }
            }
        }
    }
}
