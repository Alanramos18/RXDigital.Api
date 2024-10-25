using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RXDigital.Api.Context;
using RXDigital.Api.Context.Extensions;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
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
builder.Services.AddTransient<ISocialWorkRepository, SocialWorkRepository>();
builder.Services.AddTransient<IPharmaceuticalRepository, PharmaceuticalRepository>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

var app = builder.Build();

app.UseCors("PermitirFrontend");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
