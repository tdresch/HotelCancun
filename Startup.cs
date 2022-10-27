
using HotelCancun.DataStore.MySQL;
using HotelCancun.Models.Dtos;
using HotelCancun.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //swagger configuration
            services.AddSwaggerGen(options =>
            {
                
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelCancun API", Version = "v1" });
                options.EnableAnnotations();
                options.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<FilterBookingDtoSample>();
            

            services.AddDbContext<BookingContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("BookingConnection"));
                options.EnableDetailedErrors(true);
            });
            services.AddTransient<IBookingService, BookingService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelCancun API"));
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
