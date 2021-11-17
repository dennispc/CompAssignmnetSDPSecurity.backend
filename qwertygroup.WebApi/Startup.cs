using CompAssignmnetSDPSecurity.DataAccess;
using CompAssignmnetSDPSecurity.Security;
using CompAssignmnetSDPSecurity.Security.Models;
using CompAssignmnetSDPSecurity.WebApi.Extensions;
using CompAssignmnetSDPSecurity.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using qwertygroup.WebApi.PolicyHandlers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompAssignmnetSDPSecurity.Security.Services;

namespace CompAssignmnetSDPSecurity.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddSwaggerGen(options =>
                        {
                            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Innotech.LegosforLife.WebApi", Version = "v1" });
                            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.ApiKey,
                                Scheme = "Bearer",
                                BearerFormat = "JWT",
                                In = ParameterLocation.Header,
                                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                            });
                            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                            });
                        });
            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddApplicationServices();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db");
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });


            services.AddSingleton<IAuthorizationHandler, CanWriteProductsHandler>();
            services.AddSingleton<IAuthorizationHandler, CanReadProductsHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(CanWriteProductsHandler),
                    policy => policy.Requirements.Add(new CanWriteProductsHandler()));
                options.AddPolicy(nameof(CanReadProductsHandler),
                    policy => policy.Requirements.Add(new CanReadProductsHandler()));
            });
            services.AddCors(opt => opt
                .AddPolicy("dev-policy", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));
                services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            services.AddScoped<IAuthService,AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            MainDbContext context,
            AuthDbContext authDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "qwertygroup.WebApi v1"));
                new DbSeeder(context).SeedDevelopmentDb();
            }

            authDbContext.Database.EnsureDeleted();
            authDbContext.Database.EnsureCreated();
            authDbContext.LoginUsers.Add(new LoginUser
            {
                UserName = "dp",//not copy paste at all ;)
                HashedPassword = "123456",
                DbUserId = 1,
            });
            authDbContext.LoginUsers.Add(new LoginUser
            {
                UserName = "jb",
                HashedPassword = "123456",
                DbUserId = 2,
            });
            authDbContext.Permissions.AddRange(new Permission()
            {
                Name = "CanWriteProducts"
            }, new Permission()
            {
                Name = "CanReadProducts"
            });
            authDbContext.SaveChanges();
            authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 1, UserId = 1 });
            authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 1 });
            authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 2 });
            authDbContext.SaveChanges();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}