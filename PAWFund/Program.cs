﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Data.Entity;
using Repository.Interface;
using Repository.Models;
using Repository.Repository;
using Services.Helper;
using Services.Interface;
using Services.Models.Response;
using Services.Profiles;
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
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IUserServices, UserServices>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			// Define OData Model
			var modelBuilder = new ODataConventionModelBuilder();
			modelBuilder.EntitySet<EventResponse>("Events");
			modelBuilder.EntitySet<EventResponse>("UserEvents");

			// Configure OData
			builder.Services.AddControllers().AddOData(options =>
			{
				options.AddRouteComponents("odata", modelBuilder.GetEdmModel())
					   .Select()
					   .Filter()
					   .OrderBy()
					   .Expand()
					   .Count()
					   .SetMaxTop(null);
			});

			builder.Services.AddScoped<IEventService, EventService>();
			builder.Services.AddScoped<IEventRepository, EventRepository>();
			
			builder.Services.AddScoped<IUserEventService, UserEventService>();
			builder.Services.AddScoped<IUserEventRepository, UserEventRepository>();

			builder.Services.AddScoped<IPetService, PetService>();
			builder.Services.AddScoped<IPetRepository, PetRepository>();

			builder.Services.AddScoped<IDonationServices, DonationServices>();
			builder.Services.AddScoped<IDonationRepository, DonationRepository>();

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddSingleton(sp =>
					sp.GetRequiredService<IOptions<AppSetting>>().Value);

			builder.Services.AddScoped<IShelterService, ShelterService>();
			builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
			builder.Services.AddAutoMapper(typeof(EventProfile));

			builder.Services.AddScoped<IEmailService, EmailService>();

			builder.Services.AddScoped<IAdoptionRepository, AdoptionRepository>();
			builder.Services.AddScoped<IAdoptionService, AdoptionService>();

			builder.Services.AddScoped<IImageServices, ImageServices>();
			builder.Services.AddScoped<IImageRepository, ImageRepository>();

			builder.Services.AddScoped<IPaymentServices, PaymentServices>();
			builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

			builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
			var secretKey = builder.Configuration["AppSettings:SecretKey"];
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = builder.Configuration["AppSettings:Issuer"],
						ValidAudience = builder.Configuration["AppSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"])),
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
					};
				});

			builder.Services.AddDbContext<PawFundDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("PawFundDatabase")));

			builder.Services.AddAuthorization(opt =>
			{
				opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
				opt.AddPolicy("Shelter_Staff", policy => policy.RequireRole("Shelter_Staff"));
				opt.AddPolicy("Adopters", policy => policy.RequireRole("Adopters"));
				opt.AddPolicy("Volunteers", policy => policy.RequireRole("Volunteers"));
				opt.AddPolicy("Donors", policy => policy.RequireRole("Donors"));
			});

			// Add CORS Policy
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowLocalhost3000", policy =>
				{
					policy.WithOrigins("http://localhost:3000")
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token in the form 'Bearer {token}'",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
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
						new string[] { }
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

			// Enable CORS with the specified policy
			app.UseCors("AllowLocalhost3000");

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
