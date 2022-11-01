using FluentValidation;
using ManagerEmployee.Common.Common;
using ManagerEmployee.Common.Common.TokenService;
using ManagerEmployee.Database.Repositorys;
using ManagerEmployee.Database.Services;
using ManagerEmployee.EfCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployeeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ILoggerFactory loggerFactory;
        }

        public IConfiguration Configuration { get; }

        static class ConfigurationManager
        {
            public static IConfiguration AppSetting
            {
                get;
            }
            static ConfigurationManager()
            {
                AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BALabTestContext>(item => item.UseSqlServer("Server=PM-SANGNV;Database=BALabTest;Trusted_Connection=True;"));
            services.AddTransient<IRepositoryService<Employee>, RepositoryService<Employee>>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ITokenServiceJWT, TokenServiceJWT>();
            services.AddTransient<ILoggerFactory, LoggerFactory>();
            services.AddScoped<IValidator<Employee>, EmployeeValidator>();
            services.AddTransient<Security>();
            services.AddControllers();
            services.AddSingleton(Log.Logger);
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
            
            app.UseStaticFiles();
            app.UseAuthentication();
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
