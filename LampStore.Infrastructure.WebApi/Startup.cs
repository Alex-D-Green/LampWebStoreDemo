using FluentValidation.AspNetCore;

using LampStore.Infrastructure.CompositionRoot;
using LampStore.Infrastructure.WebApi.ApiModels;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace LampStore.Infrastructure.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PairOfLampsApiValidator>());
            //After adding Fluent Validation into pipeline all ApiModels that get into all controllers' actions
            //will be automatically checked by their validators (see in the models' files).
            //If check is failed then the client automatically receive BadRequest error.

            const string allowAllOrigin = "AllowAllOrigin";

            services.AddCors(options =>
            { 
                options.AddPolicy(allowAllOrigin, builder => builder.AllowAnyOrigin());
            });

            services.Configure<MvcOptions>(options =>
            { //That's unsafe and used only for demo purposes
                options.Filters.Add(new CorsAuthorizationFilterFactory(allowAllOrigin));
            });

            //Injection from the composition root
            services.AddFromCompositionRoot(Configuration, mc => mc.AddProfile<WebAppMappingProfile>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async (context) =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    //It's just an example, probably all request data should be logged as well
                    logger.LogCritical(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Path);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(new ErrorDetailsApi {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToJson());
                });
            });

            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
