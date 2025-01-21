using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.Mappings.AutoMapper;
using BagApp.Data.UnitOfWork;
using BagApp.Data.Validators.FluentValidaton;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BagApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BagContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));

            });

            services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
            // services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
            services
    .AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCulteres = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR"),
                    new CultureInfo("ar-SA"),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
                options.SupportedCultures = supportedCulteres;
                options.SupportedUICultures = supportedCulteres;
                //options.RequestCultureProviders.Clear();


            });



            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {

                options.Cookie.Name = "BagWorld";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.LoginPath = "/Authorize/Account/SignIn";
                options.LogoutPath = "/Authorize/Account/Logout";
                options.AccessDeniedPath = "/Authorize/Account/AccessDenied";

            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new BagProfile());
            });
            var mapper = configuration.CreateMapper();
            services.AddScoped<IUow, Uow>();
            services.AddTransient<IValidator<ProductDto>, ProductValidator>();


            services.AddTransient<IValidator<CategoryDto>, CategoryValidator>();
            services.AddTransient<IValidator<CreateCategoryDto>, CategoryCreateValidator>();
            services.AddTransient<IValidator<UpdateCategoryDto>, CategoryUpdateValidator>();


            services.AddTransient<IValidator<UserDto>, UserValidator>();
            services.AddTransient<IValidator<BannerDto>, BannerValidator>();
            services.AddTransient<IValidator<ReferenceDto>, ReferenceValidator>();
            services.AddTransient<IValidator<SubcategoryDto>, SubcategoryValidator>();
            services.AddTransient<IValidator<QuestionDto>, QuestionDtoValidator>();
            services.AddSingleton(mapper);
            services.AddSession();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseCookiePolicy();
            app.UseSession();
            app.UseNotyf();

            app.UseEndpoints(endpoints =>
            {




                endpoints.MapControllerRoute(
                 name: "areas",
                   pattern: "{Area}/{Controller}/{Action}/{id?}"
               );


                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //  name: "Default",
                //  pattern: "{Controller=Home}/{Action=Index}"

                // );


                endpoints.MapControllerRoute(
                   name: "Areas",
                   pattern: "{Area=Authorize}/{Controller=Account}/{Action=SignIn}/{id?}"
                   );

                endpoints.MapRazorPages();

            });
        }
    }
}
