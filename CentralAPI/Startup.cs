using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Repositories.Repository;
using CentralAPI.Services.IServices;
using CentralAPI.Services.Services;
using CentralAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PrivateParkAPI.Data;
using PublicParkAPI.Data;


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

            //services.AddDbContext<PublicParkContext>(options =>
            //   options.UseSqlServer(
            //       Configuration.GetConnectionString("PublicConnection")));
            //services.AddDbContext<PrivateParkContext>(options =>
            //   options.UseSqlServer(
            //       Configuration.GetConnectionString("PrivateConnection")));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IParkingLotRepository, ParkingLotRepository>();
            services.AddTransient<IParkingLotService, ParkingLotService>();
            services.AddTransient<IParkingSpotService, ParkingSpotService>();
            services.AddTransient<ICentralReservationRepository, CentralReservationRepository>();
            services.AddTransient<ICentralReservationService, CentralReservationService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISubletService, SubletService>();
            services.AddTransient<ISubletRepository, SubletRepository>();

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(typeof(Maps));
            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
