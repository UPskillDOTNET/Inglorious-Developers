using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PublicParkAPI.Authentication;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.Mappings;
using PublicParkAPI.Repositories;
using PublicParkAPI.Repositories.Repository;
using PublicParkAPI.Services;
using PublicParkAPI.Services.IServices;
using PublicParkAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicParkAPI
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
            services.AddDbContext<PublicParkContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IParkingLotRepository, ParkingLotRepository>();
            services.AddTransient<IParkingSpotRepository, ParkingSpotRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
     
            services.AddTransient<IParkingSpotService, ParkingSpotService>();
            services.AddTransient<IParkingLotService, ParkingLotService>();
            services.AddTransient<IReservationService, ReservationService>();
            // For Identity  
            // Adding Authentication  
            services.AddAuthentication("Bearer")
                     .AddIdentityServerAuthentication("Bearer", options =>
                     {
                         options.ApiName = "PubAPI";
                         options.Authority = "https://localhost:44309";
                     });

            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Maps));
            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
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

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
