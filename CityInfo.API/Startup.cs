using CityInfo.API.Interfaces;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            /*
             * .Net Core v1 Implementation 
             * We need to pass IHostingEnviroment env on the dependency injection
             */
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appSettings.json",optional : false ,reloadOnChange: true);

            //Configuration = builder.Build();
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                //.AddMvcOptions(o => o.OutputFormatters.Add(
                //    new XmlDataContractSerializerOutputFormatter()
                //    )
                //)
                //.AddJsonOptions( o => {
                //    if(o.SerializerSettings.ContractResolver != null)
                //    {
                //        var casterResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                //        casterResolver.NamingStrategy = null;
                //    }
                //})
                ;
        #if DEBUG
            services.AddTransient<ILocalMailService, LocalMailService>();
        #else
            services.AddTransient<ILocalMailService, CloudMailService>();
        #endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
