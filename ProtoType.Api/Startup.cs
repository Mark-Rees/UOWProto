using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using ProtoType.Api.Mappings;
using ProtoType.Business.Services;
using ProtoType.Core.Interfaces.Repositories;
using ProtoType.Core.Interfaces.Services;
using ProtoType.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace ProtoType.Api
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
            DapperPlusManager.AddLicense(Configuration.GetValue<string>("Z.Dapper.Plus:LicenseName"), Configuration.GetValue<string>("Z.Dapper.Plus:LicenseKey"));

            if (!DapperPlusManager.ValidateLicense(out string licenseErrorMessage)) throw new Exception(licenseErrorMessage);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped((s) => new SqlConnection(Configuration.GetValue<string>("ConnectionStrings:Default")));

            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });

            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddScoped<IFixtureService, FixtureService>();
            services.AddScoped<IFixtureRepository, FixtureRepository>();

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });
            
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FixtureProfile());
            }).CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.Api", Version = "v1" });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.Api v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            // Slugify value
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
