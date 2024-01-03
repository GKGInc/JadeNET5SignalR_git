using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using JadeNET5SignalR.Data;
using JadeNET5SignalR.Models;
using JadeNET5SignalR.Hubs;

namespace JadeNET5SignalR
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jade NET5SignalR", Version = "v1" });
            });

            //services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
            //    builder
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    .WithOrigins("http://localhost:4200");
            //}));
            services.AddCors();

            services.AddSignalR();

            //services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("JAMSqlDb")));
            //services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AlereSqlDb")));
            //"JAMSqlDb": "Data Source=JADE;Initial Catalog=JAM;user id=sa;password=sql@GKG;"
            //"AlereSqlDb": "Data Source=JEP-AS1\\ALERE2012;Initial Catalog=JAM;user id=sa;password=jade121plastics#;"

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyDbContext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JadeNET5SignalR v1"));
            }

            app.UseRouting();

            //app.UseAuthorization();

            //app.UseCors("CorsPolicy");

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ReloadHub>("/reload");
                endpoints.MapHub<BroadcastHub>("/notify"); // for workcenter
                endpoints.MapHub<BroadcastHub>("/notify_workcenter");
                endpoints.MapHub<BroadcastWorkorderHub>("/notify_workorder");
                endpoints.MapHub<BroadcastHub>("/notify_information");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
