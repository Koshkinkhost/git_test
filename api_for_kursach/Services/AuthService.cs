using api_for_kursach.DTO;
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
        Task<ArtistDTO> Login(LoginViewModel loginViewModel);
        Task<ArtistDTO> LoginAdmin(LoginViewModel loginViewModel);
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
        public async Task<ArtistDTO> LoginAdmin(LoginViewModel model)
        {
            // Проверяем, что роль действительно "admin"
            if (model.Role?.ToLower() != "admin")
                throw new UnauthorizedAccessException("Недопустимая роль для администратора");

            // Получаем пользователя (или админа) по логину и роли
            var user_find = await _userRep.GetUserByLoginAsync(model.Login, model.Role);
            if (user_find is null)
            {
                throw new UserNotExistException("Администратор не найден");
            }

            // Прямая проверка пароля (НЕ хешированного)
            if (user_find.PasswordHash != model.Password)
            {
                throw new UnauthorizedAccessException("Неверный пароль администратора");
            }

            // Устанавливаем куки с ролью "admin"
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, model.Login),
        new Claim(ClaimTypes.Role, "admin")
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            // Возвращаем либо пустой ArtistDTO, либо создаём отдельный AdminDTO
            return new ArtistDTO
            {
                Id = user_find.UserId,
                name = user_find.Username
            };
        }



        public async Task<ArtistDTO> Login(LoginViewModel model)
        {
            var user_find =await _userRep.GetUserByLoginAsync(model.Login,model.Role);
            if(user_find is null)
            {
              
                throw new UserNotExistException("User is not exist");
            }
            
         
                var password_checker = new PasswordHasher<User>();
                var check = password_checker.VerifyHashedPassword(user_find, user_find.PasswordHash, model.Password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Login),
                    new Claim(ClaimTypes.Role, model.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return await _authRep.GeId(model.Login);

            
            throw  new UserNotExistException("Some error");




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
