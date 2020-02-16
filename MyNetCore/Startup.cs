using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyNetCore.Middleware;
using Reponsitory;
using Service;
using Microsoft.OpenApi.Models;
using System.IO;
using MyNetCore.Filter;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.HttpOverrides;

namespace MyNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }
        public static ILoggerRepository repository;
        public static readonly SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("need_to_get_this_from_enviroment"));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.KnownProxies.Add(IPAddress.Parse("101.132.100.25"));
            //});
            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSubdomain",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            //身份认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       //NameClaimType = JwtClaimTypes.Name,
                       //RoleClaimType = JwtClaimTypes.Role,
                       ValidateIssuer = true,//是否验证Issuer
                       ValidateAudience = true,//是否验证Audience
                       ValidateLifetime = true,//是否验证失效时间
                       ValidateIssuerSigningKey = true,//是否验证SecurityKey
                       ValidIssuer = "MyNetCore",
                       ValidAudience = "api",
                       IssuerSigningKey = symmetricKey


                       /***********************************TokenValidationParameters的参数默认值***********************************/
                       // RequireSignedTokens = true,
                       // SaveSigninToken = false,
                       // ValidateActor = false,
                       // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                       // ValidateAudience = true,
                       // ValidateIssuer = true, 
                       // ValidateIssuerSigningKey = false,
                       // 是否要求Token的Claims中必须包含Expires
                       // RequireExpirationTime = true,
                       // 允许的服务器时间偏移量
                       // ClockSkew = TimeSpan.FromSeconds(300),
                       // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                       // ValidateLifetime = true
                   };
               });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                //启用auth支持
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // 添加swagger对身份认证的支持
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                                    },
                                    new string[] { }
                                }
                            });

            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ResultFilter>();
                options.Filters.Add<ActionFilter>();
                options.Filters.Add<MyAuthorizeFilter>();
            }).AddControllersAsServices().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var conn = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SysDbContext>(options => options.UseSqlServer(conn), ServiceLifetime.Scoped);
            //services.AddDbContext<SysDbContext>(options => options.UseMySQL(conn), ServiceLifetime.Scoped);
            services.BatchRegisterService(new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.Load("Service") }, null, ServiceLifetime.Scoped);


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("AllowSubdomain");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
