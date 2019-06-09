using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoruReactAPICore22
{
    public class Startup
    {
        public static string _InMemoryDatabase = "DoruDB";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DoruContext>(options =>
            //    options.UseInMemoryDatabase(_InMemoryDatabase));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)

                .ConfigureApiBehaviorOptions(options =>
                {
                    //To disable the default behavior (The [ApiController] attribute applies an inference rule when an action parameter 
                    //is annotated with the [FromForm] attribute: the multipart/form-data request content type is inferred), 
                    //set SuppressConsumesConstraintForFormFileParameters to true
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    //To disable binding source inference, set SuppressInferBindingSourcesForParameters to true.
                    //Add the following code in Startup.ConfigureServices after services.AddMvc().SetCompatibilityVersion
                    options.SuppressInferBindingSourcesForParameters = true;
                    //To disable the automatic 400 behavior, set the SuppressModelStateInvalidFilter property to true.
                    //Add the following highlighted code in Startup.ConfigureServices after services.AddMvc().SetCompatibilityVersion
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                    //To change the default response to SerializableError, 
                    //set the SuppressUseValidationProblemDetailsForInvalidModelStateResponses property to true in Startup.ConfigureServices
                    options.SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;
                    options.ClientErrorMapping[404].Link =
                        "https://httpstatuses.com/404";
                    //To customize the response that results from a validation error, 
                    //use InvalidModelStateResponseFactory. Add the following highlighted code after services.AddMvc().SetCompatibilityVersion
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "https://contoso.com/probs/modelvalidation",
                            Title = "One or more model validation errors occurred.",
                            Status = StatusCodes.Status400BadRequest,
                            Detail = "See the errors property for details.",
                            Instance = context.HttpContext.Request.Path
                        };

                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };

                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
