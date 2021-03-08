using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Repositories.Repository;
using CentralAPI.Repositories.Repository.PaymentRepositories;
using CentralAPI.Services.IServices;
using CentralAPI.Services.IServices.IPaymentServices;
using CentralAPI.Services.Services;
using CentralAPI.Services.Services.PaymentServices;
using CentralAPI.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CentralAPI
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
            services.AddDbContext<CentralAPIContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<CentralAPIContext>()
               .AddDefaultTokenProviders();
            services.AddAplicationRepo();
            services.AddAplicationService();
            services.AddTransient<QRgenerator>();
            services.AddTransient<EmailService>();
            services.AddTransient<ClientHelper>();
          
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(typeof(Maps));
            services.AddAuthentication("Bearer")
                 .AddIdentityServerAuthentication("Bearer", options =>
                {
                 options.ApiName = "CentralAPI";
                 options.Authority = "https://localhost:5001";
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                });
            services.AddControllers();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}