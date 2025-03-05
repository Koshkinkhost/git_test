using api_for_kursach.Models;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using api_for_kursach.Exceptions;
using Microsoft.Identity.Client;
using Azure.Core;
namespace api_for_kursach.Services
{
    public interface IUserService
    {
        Task<RegistrationResponse> Login(LoginViewModel login);
        Task<RegistrationResponse> Registration(RegistrationRequest login);
    }

    public class UserService(AppliContext _context, IHttpContextAccessor httpCon) : IUserService
    {


        public async Task<RegistrationResponse> Login(LoginViewModel login)
        {
            Console.WriteLine($"Received Login: {login.Login}, Role: {login.Role}, Password: {login.Password}");

            var user = await _context.Artists.FirstOrDefaultAsync(u => u.Login == login.Login && u.Password == login.Password && u.Role.name == login.Role);
            if (user == null)
            {

                throw new UserNotExistException("Login or password is incorrect or user does not exist");
                //return new RegistrationResponse
                //{
                //    Success = false,
                //    messages = new Dictionary<string, string[]>
                //    {
                //        { "Errors", new string[] { "Login or password is incorrect or user does not exist" } }
                //    }
                //};
            }






            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Login),
                new Claim(ClaimTypes.Role, login.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            httpCon.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return new RegistrationResponse
            {
                Success = true,
                messages = new Dictionary<string, string[]>
                {
                    { "Messages", new string[] { "Login successful" } }
                }
            };
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest login)
        {
            var user =  _context.Artists.FirstOrDefault(u => u.Login == login.Login);
            if (user is not null)
            {
                throw new LoginIsBusyException("Login is already taken");

            }
            PasswordHasher<Artist> hasher = new PasswordHasher<Artist>();
            Artist artist = new Artist() { Login = login.Login, Password = login.Password, RoleId = _context.Roles.FirstOrDefault(u => u.name == "user").id };
            _context.Artists.Add(artist);
           await  _context.SaveChangesAsync();

            return new RegistrationResponse
            {
                Success = true,
                messages = new Dictionary<string, string[]>
                {
                    { "Messages", new string[] { "Login successful" } }
                }
            };
        }
    

       
    }
    public static class ServiceProviderUserExtensions
    {
        public static void AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
