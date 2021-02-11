using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrivateParkAPI.Data;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Repositories.Repository;
using PrivateParkAPI.Services.IServices;
using PrivateParkAPI.Services.Services;
using IdentityServer4.AccessTokenValidation;


namespace PrivateParkAPI
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
            services.AddDbContext<PrivateParkContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IParkingLotRepository, ParkingLotRepository>();
            services.AddTransient<IParkingSpotRepository, ParkingSpotRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();

            services.AddTransient<IParkingSpotService, ParkingSpotService>();
            services.AddTransient<IParkingLotService, ParkingLotService>();
            services.AddTransient<IReservationService, ReservationService>();


            // Adding Authentication  
            services.AddAuthentication("Bearer")
                     .AddIdentityServerAuthentication("Bearer", options =>
                     {
                         options.ApiName = "PrivateAPI";
                         options.Authority = "https://localhost:44309";
                     });



            services.AddControllers();
            services.AddAutoMapper(typeof(Maps));
            /*services.AddAutoMapper(Assembly.GetAssembly(typeof(Profile)));*/ //If you have other mapping profiles defined, that profiles will be loaded too.
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
