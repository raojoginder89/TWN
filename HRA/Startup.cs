using AutoMapper;
using HRA.BLL;
using HRA.Common;
using HRA.Common.Enums;
using HRA.Common.Helpers;
using HRA.Common.Interfaces;
using HRA.Contracts;
using HRA.DAL;
using HRA.DAL.Entity;
using HRA.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace HRA
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
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    //var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    //var exception = exceptionHandlerPathFeature.Error;
                    var errors = GetContractValidationMessageData(context.ModelState);
                    var errDetails = new ErrorDetails
                    {
                        Message = "Contract Validation failed",
                        Data = errors
                    };

                    Log.Error($"Contract Validation failed : {errors}");
                    var result = new BadRequestObjectResult(errDetails);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            #region JWT auth

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

                auth.AddPolicy(AccessPolicy.SuperAdminLevel, policy => policy.RequireRole(RoleType.SuperAdmin.ToString()));
                auth.AddPolicy(AccessPolicy.AdminLevel, policy => policy.RequireRole(RoleType.SuperAdmin.ToString(), RoleType.Admin.ToString()));
                auth.AddPolicy(AccessPolicy.GroupLevel, policy => policy.RequireRole(RoleType.SuperAdmin.ToString(), RoleType.Admin.ToString(), RoleType.Group.ToString()));
            });

            services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("HRA")));

            services.AddDefaultIdentity<HRA.DAL.Entity.User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<MainDbContext>()
                .AddDefaultTokenProviders();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Reset password token expiry time
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(24));

            #endregion

            //services.AddMvc(options => { options.Filters.Add(new ApiExceptionFilter()); });
            #region AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelContractMapper());
                mc.AddProfile(new ModelEntityMapper());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region DependencyInjection

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IUnitOfWork>(ctx => new EFUnitOfWork(ctx.GetRequiredService<MainDbContext>()));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserVideosService, UserVideosService>();
            services.AddTransient<IVideosService, VideosService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IHRAService, HRAService>();
            services.AddTransient<IAddressService, AddressService>();

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HMA API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<DAL.Entity.User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            InitDatabase(app);

            SeedData.SeedAdminUser(userManager);
            app.UseHttpsRedirection();
            app.ConfigureExceptionMiddleware();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMA API V1");
            }); app.UseAuthentication();
        }

        private static void InitDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<MainDbContext>();
            context.Database.Migrate();
        }

        private static string[] GetContractValidationMessageData(ModelStateDictionary modelState)
        {
            var data = new List<string>();

            foreach (var (key, value) in modelState)
            {
                var errors = new List<string>();
                foreach (var error in value.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                        errors.Add(error.ErrorMessage);
                    else if (error.Exception != null && !string.IsNullOrEmpty(error.Exception.Message))
                        errors.Add("Error parsing value");
                }

                if (errors.Count > 0)
                    data.Add(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", key, string.Join(",", errors)));
            }

            return data.ToArray();
        }
    }
}
