using api_for_kursach.Models;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using api_for_kursach.Services;
using api_for_kursach.Exceptions;
using System.Linq.Expressions;

namespace api_for_kursach.Controllers
{
    public class AccountController(AppliContext context, IUserService user,IRegistrationResponseFactory factory) : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return Ok("запрос выполнен");
        }

        [HttpPost]
        public async Task<RegistrationResponse> Registration([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return factory.CreateFailureResponse(ModelState.Where(x => x.Value.Errors.Count > 0).
                ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
                //return new RegistrationResponse
                //{
                //    Success = false,
                //    messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                //    ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                //};
            }
            try
            {
                var result = user.Registration(request);

                return result;
            }
            catch(Exception ex)
            {
                switch (ex)
                {
                    case LoginIsBusyException busyException:
                        return factory.CreateFailureResponse(ModelState.Where(x => x.Value.Errors.Count > 0).
                            ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()));
                        
                    default:
                        return factory.CreateFailureResponse(new Dictionary<string, string[]>
                                 {
                                        { "Errors", new string[] { "An unexpected error occurred. Please try again later." } }
                                 });
                        
                }
            }
        }
        [HttpPost]
        public async Task<RegistrationResponse> Login([FromBody] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await user.Login(login);
                    return response;

                }
                catch (Exception ex)
                {
                    switch (ex)
                    {
                        case UserNotExistException exception:
                            return factory.CreateFailureResponse(new Dictionary<string, string[]>
                                {
                                    { "Errors", new string[] { "Login or password is incorrect or user does not exist" } }
                                });
                           
                        case NoAdminRights noadmin:
                            return factory.CreateFailureResponse(new Dictionary<string, string[]>
                            {
                                { "Errors", new string[] { "You do not have admin rights" } }
                            });
                           
                        default:

                            return factory.CreateFailureResponse(new Dictionary<string, string[]>
                                 {
                                        { "Errors", new string[] { "An unexpected error occurred. Please try again later." } }
                                 });
                           


                    }
                }



            }
            else
            {
                return factory.CreateFailureResponse(new Dictionary<string, string[]>
                        {
                            { "Errors", new string[] { "Data is incorrect" } }
                        });
               
            }

        }
        [HttpPost]
        public RegistrationResponse CheckRights([FromBody] LoginViewModel request)
        {
            var user = context.Artists.FirstOrDefault(u => u.Login == request.Login && u.RoleId == 1);
            if (user is null)
            {
                return factory.CreateFailureResponse(new Dictionary<string, string[]>
                    {
                            { "Errors", new string[] { "You do not have an admin rights" } }
                    });
                
            }
            return factory.CreateSuccessResponse(new Dictionary<string, string[]>
                    {
                            { "Messages", new string[] { "You are admin" } }
                    });
           
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUserRole()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            return Ok(new { role = user });
        }
        [HttpPost]
        public ActionResult GetName([FromBody] string name)
        {
            var user_name = context.Artists.FirstOrDefault(u => u.Login == name);
            if (user_name is null)
            {
                return BadRequest("Нет пользователя с таким именем");
            }
            return Json(user_name);
        }
        [HttpPost]
        public async void LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
        public void Authentificate()
        {

        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult CheckAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.Out.WriteLine("пользователь зареган");
                return Ok(new { isAuthenticated = true, username = User.Identity.Name });
            }
            Console.Out.WriteLine("пользователь НЕ  зареган");
            return Ok(new { isAuthenticated = false });
        }
    }
}
