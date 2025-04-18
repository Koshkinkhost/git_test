using api_for_kursach.Exceptions;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace api_for_kursach.Services
{
    public interface IAuthService
    {
        Task Login(LoginViewModel loginViewModel);
        Task Registration(RegistrationRequest model);
        bool CheckAuthAsync();

    }
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRep;
        private readonly IUserRepository _userRep;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IAuthRepository authRep, IUserRepository userRep, IHttpContextAccessor httpContextAccessor)
        {
            _authRep = authRep;
            _userRep = userRep;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CheckAuthAsync()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

        public async Task Login(LoginViewModel model)
        {
            var user_find =await _userRep.GetUserByLoginAsync(model.Login,model.Role);
            if(user_find is null)
            {
                throw new UserNotExistException("User is not exist");
            }
            
            if (model.Role == "artist")
            {
                var password_checker = new PasswordHasher<User>();
                var check = password_checker.VerifyHashedPassword(user_find, user_find.PasswordHash, model.Password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Login),
                    new Claim(ClaimTypes.Role, model.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            }
           


        }
        public async Task Registration(RegistrationRequest model)
        {
            var user_find=await _userRep.GetUserByLoginAsync(model.Login);
            if(user_find is not null)
                throw new LoginIsBusyException("пользователь уже есть");


            PasswordHasher<User> hasher = new PasswordHasher<User>();
            User user = new User();
            var hashpass = hasher.HashPassword(user, model.Password);
            user.Username = model.Login;
            user.PasswordHash = hashpass;
            await _authRep.AddUserAsync(user.Username, user.PasswordHash);

        }

        public Task Registration()
        {
            throw new NotImplementedException();
        }
    }
}
