﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RXDigital.Api.DTOs;
using RXDigital.Api.Entities;
using RXDigital.Api.Helpers;
using RXDigital.Api.Repositories.Interfaces;
using RXDigital.Api.Services.Interfaces;

namespace RXDigital.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPharmaceuticalRepository _pharmaceuticalRepository;
        private readonly ILogger<AccountService> _logger;
        private readonly JwtSettings _jwtSettings;

        public AccountService(IAccountRepository accountRepository, IDoctorRepository doctorRepository,
            IPharmaceuticalRepository pharmaceuticalRepository, IOptions<JwtSettings> jwtSettings, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _doctorRepository = doctorRepository;
            _pharmaceuticalRepository = pharmaceuticalRepository;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<string> RegisterAsync(RegisterRequestDto accountDto, CancellationToken cancellationToken)
        {
            try
            {
                var emailExists = await _accountRepository.CheckRegisteredEmailAsync(accountDto.Email, cancellationToken);

                if (emailExists)
                    return null;

                var account = accountDto.Convert();

                var result = await _accountRepository.CreateAsync(account, accountDto.Password);

                switch (accountDto.RoleId)
                {
                    case RoleEnum.Doctor:

                        var newDoctor = new Doctor
                        {
                            RegistrationId = (Int32)accountDto.Registration,
                            UserId = account.Id,
                            EspecialidadId = (Int32)accountDto.EspecialidadId
                        };

                        await _doctorRepository.AddAsync(newDoctor, cancellationToken);
                        await _doctorRepository.SaveChangesAsync(cancellationToken);

                        break;

                    case RoleEnum.Pharmaceutic:

                        var newPharmaceutic = new Pharmaceutical
                        {
                            RegistrationId = (Int32)accountDto.Registration,
                            AccountId = account.Id
                        };

                        await _pharmaceuticalRepository.AddAsync(newPharmaceutic, cancellationToken);
                        await _pharmaceuticalRepository.SaveChangesAsync(cancellationToken);

                        break;
                }

                if (!result.Succeeded)
                {
                    throw new ApplicationException("Error creating user");
                }

                return account.Id;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        /// <inheritdoc />
        public async Task<string> LoginAsync(LoginRequestDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.FindByEmailAsync(loginDto.Email, cancellationToken);

            if (user == null || !await _accountRepository.CheckPasswordAsync(user, loginDto.Password))
            {
                _logger.LogInformation($"The email: {loginDto.Email} has try to log in with password: {loginDto.Password}");
                throw new Exception("El usuario o la contraseña son invalidas. Por favor intente de nuevo.");
            }

            switch (user.Estado)
            {
                case 0:
                    throw new Exception("El usuario esta pendiente de aprobación.");
                    break;
                case 2:
                    throw new Exception("El usuario esta rechazado. Pruebe registrandose de nuevo!");
                    break;
                default:
                    break;
            }

            // We need to update claims in the future.
            var authClaims = new List<Claim>()
            {
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("UserId", user.Id.ToString()),
            };

            var jwt = CreateToken(authClaims);

            return jwt;
        }

        private string CreateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

    public enum RoleEnum
    {
        Admin = 1,
        Doctor = 2,
        Pharmaceutic = 3
    }
}
