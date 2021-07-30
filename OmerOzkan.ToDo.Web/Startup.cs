using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OmerOzkan.ToDo.Business.Containers.MicrosoftIoC;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.Extensions;

namespace OmerOzkan.ToDo.Web
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
            services.AddDependencies();
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-9TOE094;Database=ToDoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            });

            services.AddCustomIdentity();
            services.AddAutoMapper(typeof(Startup));
            services.AddCustomValidator();          
            services.AddControllersWithViews().AddFluentValidation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            //IdentityInitializer.SeedData(userManager, roleManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
