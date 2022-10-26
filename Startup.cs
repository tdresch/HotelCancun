using HotelCancun.Service;
using HotelCancunAPI.HotelCancun.DataStore.MySQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace HotelCancun
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelCancun_InMemory", Version = "v1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "HotelCancun_Database", Version = "v2" });
            });
            services.AddDbContext<BookingContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("BookingConnection"));
                options.EnableDetailedErrors(true);
            });
            services.AddTransient<IBookingService, BookingService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelCancun v1"));
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "HotelCancun v2"));
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
