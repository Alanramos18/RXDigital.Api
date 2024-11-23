using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RXDigital.Api.Context;
using RXDigital.Api.Context.Extensions;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Helpers;
using RXDigital.Api.Repositories;
using RXDigital.Api.Repositories.Interfaces;
using RXDigital.Api.Services;
using RXDigital.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        //policy.WithOrigins("http://localhost:4200/", "https://localhost:4200/") // Especifica los orígenes permitidos
        policy.AllowAnyOrigin()
              .AllowAnyMethod()  // Permitir cualquier método
              .AllowAnyHeader(); // Permitir cualquier encabezado
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:61768/",
            ValidAudience = "http://localhost:8100/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RickyfortElComandante2024Argentina"))
        };

        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Autenticación fallida: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validado");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireClaim("roleId", "1");
    });

    options.AddPolicy("MedicPolicy", policy =>
    {
        policy.RequireClaim("roleId", "2");
    });

    options.AddPolicy("PharmaceuticPolicy", policy =>
    {
        policy.RequireClaim("roleId", "3");
    });
});

builder.Services.AddControllers();
builder.Services.AddContextConfiguration(builder.Configuration);

builder.Services.AddIdentity<AccountEntity, IdentityRole>()
    .AddEntityFrameworkStores<RxDigitalContext>()
    .AddUserManager<AccountRepository>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(x => {
    x.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddTransient<IMedicineRepository, MedicineRepository>();
builder.Services.AddTransient<IPrescriptionMedicineRepository, PrescriptionMedicineRepository>();
builder.Services.AddTransient<ISocialWorkRepository, SocialWorkRepository>();
builder.Services.AddTransient<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddTransient<IPharmaceuticalRepository, PharmaceuticalRepository>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IPharmaceuticalService, PharmaceuticalService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailConfiguration"));

var app = builder.Build();

app.UseCors("PermitirFrontend");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
