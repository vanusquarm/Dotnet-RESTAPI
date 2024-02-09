using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication.API.Persistence;
using WebApplication.API.Repositories;
using WebApplication.API.Repositories.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;

namespace WebApplication.API
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
            // services.AddScoped(typeof(ISyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<TodoRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication.API", Version = "v1" });
            });

            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigins",
                    policy  =>
                    {
                        policy.WithOrigins("http://localhost:3000");
                    });
            });

            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            //         ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))
            //     )
            // );

            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication.API v1"));
            }

            app.UseRouting();
            
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
