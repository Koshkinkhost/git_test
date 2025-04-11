
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using api_for_kursach.Services;
using api_for_kursach.Exceptions;
using System.Linq.Expressions;

namespace api_for_kursach.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRegistrationResponseFactory _factory;

        public AccountController(IUserService userService, IRegistrationResponseFactory factory)
        {
            _userService = userService;
            _factory = factory;
        }

        // GET: AccountController
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("запрос выполнен");
        }

        // POST: Registration
        [HttpPost]
        public async Task<RegistrationResponse> Registration([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return _factory.CreateFailureResponse(ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
            }

            try
            {
                //var result = await _userService.Registration(request);
                //return result;
                return null;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case LoginIsBusyException:
                        return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                        {
                            { "Errors", new[] { "Login is already taken" } }
                        });

                    default:
                        return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                        {
                            { "Errors", new[] { "An unexpected error occurred. Please try again later." } }
                        });
                }
            }
        }

        // POST: Login
        [HttpPost]
        public async Task<RegistrationResponse> Login([FromBody] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var response = await _userService.Login(login);
                    //return response;
                    return null;
                }
                catch (Exception ex)
                {
                    switch (ex)
                    {
                        case UserNotExistException:
                            return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                            {
                                { "Errors", new[] { "Login or password is incorrect or user does not exist" } }
                            });

                        case NoAdminRights:
                            return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                            {
                                { "Errors", new[] { "You do not have admin rights" } }
                            });

                        default:
                            return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                            {
                                { "Errors", new[] { "An unexpected error occurred. Please try again later." } }
                            });
                    }
                }
            }
            else
            {
                return _factory.CreateFailureResponse(new Dictionary<string, string[]>
                {
                    { "Errors", new[] { "Data is incorrect" } }
                });
            }
        }

        // POST: Check Rights
        [HttpPost]
        public RegistrationResponse CheckRights([FromBody] LoginViewModel request)
        {
            // Add logic to check user rights here
            return _factory.CreateSuccessResponse(new Dictionary<string, string[]>
            {
                { "Messages", new[] { "You are admin" } }
            });
        }

        // GET: GetUserRole
        [Authorize]
        [HttpGet]
        public IActionResult GetUserRole()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return Ok(new { role = user });
        }

        // POST: LogOut
        [HttpPost]
        public async Task LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // GET: CheckAuth
        [HttpGet]
        public IActionResult CheckAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { isAuthenticated = true, username = User.Identity.Name });
            }
            return Ok(new { isAuthenticated = false });
        }
    }
}
