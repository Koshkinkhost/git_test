using api_for_kursach.Exceptions;

using api_for_kursach.Repositories;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api_for_kursach.Services
{
    public interface IUserService
    {
        //Task<RegistrationResponse> Login(LoginViewModel login);
        //Task<RegistrationResponse> Registration(RegistrationRequest registrationRequest);
        //Task<CheckAuthResponse> CheckAuth();
    }

    public class UserService : IUserService
    {
        //    private readonly IUserRepository _userRepository;
        //    private readonly IHttpContextAccessor _httpContextAccessor;

        //    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        //    {
        //        _userRepository = userRepository;
        //        _httpContextAccessor = httpContextAccessor;
        //    }

        //    public async Task<RegistrationResponse> Login(LoginViewModel login)
        //    {
        //        Console.WriteLine($"Received Login: {login.Login}, Role: {login.Role}, Password: {login.Password}");

        //        // Получаем пользователя по логину
        //        var user = await _userRepository.GetUserByLoginAsync(login.Login);
        //        PasswordHasher<User> hasher = new PasswordHasher<User>();
        //        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
        //        if (result != PasswordVerificationResult.Success || user.Role != login.Role)
        //        {
        //            throw new UserNotExistException("Login or password is incorrect or user does not exist");
        //        }

        //        // Если пользователь найден, создаем claims и авторизуем
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, login.Login),
        //            new Claim(ClaimTypes.Role, login.Role)
        //        };

        //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(claimsIdentity);

        //        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        return new RegistrationResponse
        //        {
        //            Success = true,
        //            messages = new Dictionary<string, string[]>
        //            {
        //                { "Messages", new string[] { "Login successful" } }
        //            }
        //        };
        //    }

        //    public async Task<RegistrationResponse> Registration(RegistrationRequest registrationRequest)
        //    {
        //        // Проверяем, существует ли уже пользователь с таким логином
        //        var existingUser = await _userRepository.GetUserByLoginAsync(registrationRequest.Login);
        //        if (existingUser != null)
        //        {
        //            throw new LoginIsBusyException("Login is already taken");
        //        }

        //        // Создаем новый объект пользователя
        //        PasswordHasher<User> hasher = new PasswordHasher<User>();
        //        var user = new User
        //        {
        //            Username = registrationRequest.Login,
        //        };
        //        user.PasswordHash = hasher.HashPassword(user, registrationRequest.Password);

        //        // Получаем роль по имени (например, "user")
        //        var role = await _userRepository.GetRoleByNameAsync("user");
        //        if (role == null)
        //        {
        //            throw new Exception("Role not found");
        //        }
        //        user.RoleId = role.RoleId;

        //        // Создаем нового артиста
        //        var artist = new Artist
        //        {
        //            Name = registrationRequest.Login,
        //        };

        //        // Создаем нового автора (например, те же данные, что и для артиста)
        //        var author = new Author
        //        {
        //            Name = registrationRequest.Login,
        //        };

        //        // Добавляем пользователя, артиста и автора
        //        await _userRepository.AddUserAsync(user, artist, author);

        //        return new RegistrationResponse
        //        {
        //            Success = true,
        //            messages = new Dictionary<string, string[]>
        //    {
        //        { "Messages", new string[] { "Registration successful" } }
        //    }
        //        };
        //    }


        //    public async Task<CheckAuthResponse> CheckAuth()
        //    {
        //        var user = _httpContextAccessor.HttpContext.User;

        //        // Проверка, авторизован ли пользователь
        //        if (user.Identity.IsAuthenticated)
        //        {
        //            // Возвращаем информацию о пользователе (например, логин и роль)
        //            var username = user.Identity.Name;
        //            var role = user.FindFirst(ClaimTypes.Role)?.Value;

        //            return new CheckAuthResponse
        //            {
        //                IsAuthenticated = true,
        //                Username = username,
        //                Role = role
        //            };
        //        }
        //        else
        //        {
        //            return new CheckAuthResponse
        //            {
        //                IsAuthenticated = false,
        //                Username = null,
        //                Role = null
        //            };
        //        }
        //    }
        //}

        //public class CheckAuthResponse
        //{
        //    public bool IsAuthenticated { get; set; }
        //    public string Username { get; set; }
        //    public string Role { get; set; }
        //}

        //public static class ServiceProviderUserExtensions
        //{
        //    public static void AddUserService(this IServiceCollection services)
        //    {
        //        services.AddScoped<IUserService, UserService>();
        //    }
        //}
    }
}
