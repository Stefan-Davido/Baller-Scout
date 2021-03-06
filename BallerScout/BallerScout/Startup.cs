using AutoMapper;
using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Mapping;
using BallerScout.Models;
using BallerScout.Repository;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BallerScoutConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // *** Repositories
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IFollowingRepository, FollowingRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<ISavedPostRepository, SavedPostRepository>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();
            services.AddTransient<IPlayerHistoryRepository, PlayerHistoryRepository>();
            services.AddTransient<ISeasonRepository, SeasonRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            
            // *** Services
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IFollowingService, FollowingService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<ISavedPostService, SavedPostService>();
            services.AddTransient<ISkillsService, SkillsService>();
            services.AddTransient<IPlayerHistoryService, PlayerHistoryService>();
            services.AddTransient<IUploadImageService, UploadImageService>();
            services.AddTransient<ISeasonService, SeasonService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IDateConverterService, DateConverterService>();
            services.AddTransient<IDropDownsService, DropDownsService>();
            services.AddTransient<IMyEmailService, MyEmailService>();

            services.AddAutoMapper(typeof(MapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
