using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.secr;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApp.Helpers;
using WebApp.dto;
using WebApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.gpn;
using System.Security.Principal;

namespace WebApp.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<user>> GetAll();
        Task<user> GetById(Guid id);
        Task<AuthenticateResponse> Registration(RegistrationModel user);
        ClaimsIdentity GetClaimsIdentity(user user_context);
    }

    public class UserService : IUserService
    {
        /// <summary>
        /// БД
        /// </summary>
        private readonly Orchestrator _orchestrator;

        private readonly AppSettings _appSettings;

        public UserService(Orchestrator orchestrator, IOptions<AppSettings> appSettings) 
        {
            _orchestrator = orchestrator;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var hash = GetHash(model.Password);

            var user = await _orchestrator.GetUser(model, hash);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse> Registration(RegistrationModel new_user)
        {
            role user_role = await _orchestrator.GetRole(new_user.Role.role_name);

            if (user_role == null)
            {
                throw new Exception("Роль пользователя не существует");
            }

            contractor user_contractor = null;
            if (user_role.role_name == "contractor")
            {
                user_contractor = await _orchestrator.GetContractor(new_user.Contractor.contractor_id);
            }
            //if (user_contractor == null)
            //{
            //    throw new Exception("Поставщик не существует");
            //}

            user user = await _orchestrator.GetUserByEmail(new_user.Email);

            if (user != null)
            {
                throw new Exception("Пользователь с таким email зарегистрирован");
            }

            user user_context = new user()
            {
                email = new_user.Email,
                user_name = new_user.Name,
                position = new_user.Position,
                contact_info = new_user.ContactInfo,
                Password = GetHash(new_user.Password),
                role_id = user_role.role_id,
                role = user_role,
        //        contractor_id = user_contractor != null ? user_contractor.contractor_id : Guid.Empty,
                contractor = user_contractor
            };

            var result = await _orchestrator.Add(user_context);
            if (result != null && result.State == EntityState.Added)
            {
                await _orchestrator.SaveChangesAsync();

                var newadded = await GetById(result.Entity.user_id);

                var token = generateJwtToken(newadded);

                return new AuthenticateResponse(newadded, token);
            }
            return null;
        }

        public async Task<IEnumerable<user>> GetAll()
        {
            return await _orchestrator.GetUsers();
        }

        public async Task<user> GetById(Guid id)
        {
            user result = await _orchestrator.GetUser(id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        // helper methods

        private string generateJwtToken(user user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsIdentity GetClaimsIdentity(user user)
        {
            return new ClaimsIdentity(new[] { 
                    new Claim("id", user.user_id.ToString()),
                    new Claim(ClaimTypes.Role, user.role.role_name),
                    new Claim(ClaimTypes.Name, user.user_name)
                });
        }

        public static Guid GetHash(string value)
        {
            return GetHash(Encoding.ASCII.GetBytes(value));
        }

        public static Guid GetHash(byte[] value)
        {
            var hasher = new System.Security.Cryptography.SHA256Managed();
            var hash = hasher.ComputeHash(value);
            return new Guid(hash.Take(16).ToArray());
        }
    }
}