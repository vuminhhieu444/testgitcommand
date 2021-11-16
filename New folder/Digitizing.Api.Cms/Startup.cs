using Library.Common.Caching;
using Library.Common.Helper;
using Library.Cms.BusinessLogicLayer;
using Library.Cms.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO.Compression;
using System.Linq;

namespace Digitizing.Api.Cms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            FileHelper.configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();
            services.TryAddSingleton<ICacheProvider, LZ4RedisCache>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDatabaseHelper, DatabaseHelper>();

			services.AddTransient<IWebsiteItemStatusRefBusiness, WebsiteItemStatusRefBusiness>();
			services.AddTransient<IWebsiteItemStatusRefRepository,WebsiteItemStatusRefRepository>();


			services.AddTransient<IWebsiteGroupTypeRefBusiness, WebsiteGroupTypeRefBusiness>();
			services.AddTransient<IWebsiteGroupTypeRefRepository,WebsiteGroupTypeRefRepository>();


			services.AddTransient<IWebsiteTagBusiness, WebsiteTagBusiness>();
			services.AddTransient<IWebsiteTagRepository,WebsiteTagRepository>();

			services.AddTransient<IWebsiteSlideBusiness, WebsiteSlideBusiness>();
			services.AddTransient<IWebsiteSlideRepository,WebsiteSlideRepository>();

			services.AddTransient<IWebsiteImageBusiness, WebsiteImageBusiness>();
			services.AddTransient<IWebsiteImageRepository,WebsiteImageRepository>();

			services.AddTransient<IWebsiteProductBusiness, WebsiteProductBusiness>();
			services.AddTransient<IWebsiteProductRepository,WebsiteProductRepository>();

			services.AddTransient<IWebsitePartnerBusiness, WebsitePartnerBusiness>();
			services.AddTransient<IWebsitePartnerRepository,WebsitePartnerRepository>();

			services.AddTransient<IWebsiteInfoRefBusiness, WebsiteInfoRefBusiness>();
			services.AddTransient<IWebsiteInfoRefRepository,WebsiteInfoRefRepository>();

			services.AddTransient<IWebsiteItemTypeRefBusiness, WebsiteItemTypeRefBusiness>();
			services.AddTransient<IWebsiteItemTypeRefRepository,WebsiteItemTypeRefRepository>();

            services.AddTransient<IItemGroupRepository, ItemGroupRepository>();
            services.AddTransient<IItemGroupBusiness, ItemGroupBusiness>();

            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemBusiness, ItemBusiness>();


            services.AddTransient<IHomeRepository, HomeRepository>();
            services.AddTransient<IHomeBusiness, HomeBusiness>();


            var MimeTypes = new[]
                                {
                                    // General
                                    "text/plain",
                                    // Static files
                                    "text/css",
                                    "application/javascript",
                                    // MVC
                                    "text/html",
                                    "application/xml",
                                    "text/xml",
                                    "application/json",
                                    "text/json",
                                    "image/svg+xml",
                                    "application/atom+xml"
                                };
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(MimeTypes); ;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = Int32.MaxValue;
                x.MultipartBodyLengthLimit = Int32.MaxValue;
                x.MultipartHeadersLengthLimit = Int32.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
        }
    }
}
