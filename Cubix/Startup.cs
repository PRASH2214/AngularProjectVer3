
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using static Cubix.Controllers.BaseController;

namespace Cubix
{
    /// <summary>
    /// Start Up Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///  Start UP Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///  Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Add Cors
            services.AddCors();

            //Add Swagger Custom Documentation with Authorize the header functionality
            services.AddSwaggerDocumentation();

            //Add Cahce Memory
            services.AddMemoryCache();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Set the Configuration Parameters in Static Class
            AppSetting.SetSetting(Configuration);

            // Add JWT Token for authenticate the requests after login 
            var key = Encoding.ASCII.GetBytes(AppSetting.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {

                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        ResultModel<string> oResultModel = new ResultModel<string>();
                        oResultModel.Success = false;
                        oResultModel.Message = "Your session has ended due to inactivity";
                        oResultModel.Status = (int)HttpStatusCode.Forbidden;
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(oResultModel));
                    },

                    OnAuthenticationFailed = context =>
                    {


                        ResultModel<string> oResultModel = new ResultModel<string>();
                        oResultModel.Success = false;
                        oResultModel.Message = "Token Expired or not matched";
                        oResultModel.Status = (int)HttpStatusCode.Unauthorized;
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(oResultModel));
                    }
                };
            });

            //suppress the default model validation error message
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // Super Admin Token Validation Filter
            services.AddScoped<SuperAdminTokenFilter>();
            // Admin Token Validation Filter
            services.AddScoped<AdminTokenFilter>();
            // Doctor Token Validation Filter
            services.AddScoped<DoctorTokenFilter>();
            // MR Profile Token Validation Filter
            services.AddScoped<MRTokenFilter>();
            // Patient Token Validation Filter
            services.AddScoped<PatientTokenFilter>();
            // Patient Profile Token Validation Filter
            services.AddScoped<PatientProfileTokenFilter>();


            // Add check model custom validation filter
            services.AddMvcCore(options =>
           {
               options.Filters.Add(typeof(ValidateModelFilter));
               options.Filters.Add(typeof(HttpGlobalExceptionFilter));
           });

            //  Add Scoped service
            ServiceToScope oServiceToScope = new ServiceToScope();
            oServiceToScope.AddToScope(services);


            //Declare Signal R
            services.AddSignalR(
                options =>
                {
                    options.EnableDetailedErrors = true;
                    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                    options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                });




        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;
                //Get Token from Header
                string token = context.Request.Headers["Authorization"];
                //If token exists in request header
                if (token != "" && token != null)
                {
                    //Decrypt back to JWT token  before JWT  middeleware check the token
                    context.Request.Headers["Authorization"] = "Bearer " + Secure.Decrypt(token.Replace("Bearer ", ""));
                }
                await next();
            });



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwaggerDocumentation(); //for documentation
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(
            options => options.WithOrigins("http://localhost:4200", "https://dev-econsultation.ecubix.com")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
             );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/ChatHub", options =>
                {
                    //options.Transports = TransportType.All;
                    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                    options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
                    options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);
                });
            });



            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
