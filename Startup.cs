using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace NavnedbAPI
{
    public static class AppSettingsClass {
        public static string MyConnection { get; set; }

    }
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Cors not needed
            // services.AddCors();

            // refer to connection string in appsettings.json
            AppSettingsClass.MyConnection = Configuration.GetConnectionString("DefaultConnection");
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Cors not needed

            // app.UseCors(
            //     options => options.AllowAnyOrigin().AllowAnyHeader()
            // );

            app.UseHttpsRedirection();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name:"default",
                    template: "{controller=Home}/{action=Index}"
                );
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            

      
            
        }
    }
}
