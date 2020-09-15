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

namespace WebApp.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<user>> GetAll();
        Task<user> GetById(int id);
        Task<AuthenticateResponse> Registration(AuthenticateResponse user, string password);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<user> _users = new List<user>
        //{
        //    new user { user_id = 1, email = "Test", user_name = "User", position = "test", Password = "test" }
        //};

        /// <summary>
        /// ад
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

        public async Task<AuthenticateResponse> Registration(AuthenticateResponse new_user, string password)
        {
            role user_role = await _orchestrator.GetRole("unknow");

            if (user_role == null)
            {
                user_role = new role() { role_name = "unknow", description = "unknow" };

                var result_role = await _orchestrator.Add(user_role);
                if (result_role != null && result_role.State == EntityState.Added)
                {
                    await _orchestrator.SaveChangesAsync();

                    user_role = await _orchestrator.GetRole("unknow");
                }
                else return null;
            }

            user user_context = new user()
            {
                user_id = new_user.Id,
                email = new_user.Email,
                user_name = new_user.Name,
                position = new_user.Position,
                Password = GetHash(password),
                role_id = user_role.role_id,
                role = user_role
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

        public async Task<user> GetById(int id)
        {
            return await _orchestrator.GetUser(id);
        }

        // helper methods

        private string generateJwtToken(user user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("id", user.user_id.ToString()),
                    new Claim("role", user.role.ToString()),
                    new Claim("name", user.user_name.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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