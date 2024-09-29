
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Data.Entity;
using Repository.Interface;
using Repository.Models;
using Repository.Repository;
using Services.Interface;
using Services.Services;
using System.Text;

namespace PAWFund
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IShelterService, ShelterService>();
            builder.Services.AddScoped<IShelterRepository, ShelterRepository>();

            builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
            var secretKey = builder.Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddDbContext<PawFundDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PawFundDatabase"))); // Assuming you're using SQL Server, adjust if using another database

            //builder.Services.AddAuthorization(opt =>
            //{
            //    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //    opt.AddPolicy("Shelter_Staff", policy => policy.RequireRole("Shelter_Staff"));
            //    opt.AddPolicy("Adopters", policy => policy.RequireRole("Adopters"));
            //    opt.AddPolicy("Volunteers", policy => policy.RequireRole("Volunteers"));
            //    opt.AddPolicy("Donors", policy => policy.RequireRole("Donors"));

            //});



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header

                        },
                    Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
