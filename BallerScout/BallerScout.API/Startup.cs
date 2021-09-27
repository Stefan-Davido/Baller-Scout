using BallerScout.Repository;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.API
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
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:BookstoreConnection"]));

            // *** Repositories
            //services.AddTransient<IPostRepository, PostRepository>();
            //services.AddTransient<IFollowingRepository, FollowingRepository>();
            //services.AddTransient<ILikeRepository, LikeRepository>();
            //services.AddTransient<ISavedPostRepository, SavedPostRepository>();
            //services.AddTransient<ISkillsRepository, SkillsRepository>();
            //services.AddTransient<IPlayerHistoryRepository, PlayerHistoryRepository>();
            //services.AddTransient<ISeasonRepository, SeasonRepository>();
            //services.AddTransient<IGameRepository, GameRepository>();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
