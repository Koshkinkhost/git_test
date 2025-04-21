
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
        private readonly IAuthService _authService;
        private readonly IRegistrationResponseFactory _factory;

        public AccountController(IUserService userService, IRegistrationResponseFactory factory, IAuthService authService)
        {
            _userService = userService;
            _factory = factory;
            _authService = authService;
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
                await _authService.Registration(request);
                return _factory.CreateSuccessResponse(new Dictionary<string, string[]>
                        {
                            { "Status", new[] { "Registration is successful" } }
                        },0);
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
        [HttpPost]
        public async Task<RegistrationResponse> LoginAdmin([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.LoginAdmin(loginViewModel);

                    return _factory.CreateSuccessResponse(
                        new Dictionary<string, string[]>
                        {
                    { "Status", new[] { "Admin login is successful" } }
                        },
                        result.Id
                    );
                }
                catch (Exception ex)
                {
                    switch (ex)
                    {
                        case UserNotExistException:
                            return _factory.CreateFailureResponse(
                                new Dictionary<string, string[]>
                                {
                            { "Errors", new[] { "Admin login or password is incorrect" } }
                                });

                        case NoAdminRights:
                            return _factory.CreateFailureResponse(
                                new Dictionary<string, string[]>
                                {
                            { "Errors", new[] { "You do not have admin rights" } }
                                });

                        default:
                            var errors = ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .ToDictionary(
                                    kvp => kvp.Key,
                                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                );

                            return _factory.CreateFailureResponse(errors);
                    }
                }
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return _factory.CreateFailureResponse(errors);
            }
        }

        [HttpPost]
        public async Task<RegistrationResponse> Login([FromBody] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var t=await _authService.Login(login);
                    return _factory.CreateSuccessResponse(new Dictionary<string, string[]>
                            {
                                { "Status", new[] { "Login is successful" } }
                            },t.Id);
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
                            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                            return _factory.CreateFailureResponse(errors);
                    }
                }
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return _factory.CreateFailureResponse(errors);
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
            },0);
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
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("ok");
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
