using api_for_kursach.Models;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api_for_kursach.Controllers
{
    public class AccountController(AppliContext context) : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return Ok("запрос выполнен");
        }
       
        [HttpPost]
        public async Task<RegistrationResponse> Registration([FromBody]RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                
                return new RegistrationResponse { 
                    Success = false, 
                    messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                    ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            };
            }
            var user = context.Artists.FirstOrDefault(u => u.Login == request.Login);
            if (user is not null)
            {
                return new RegistrationResponse { 
                    Success = false,
                    messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                    ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
            }
           Artist artist=new Artist() { Login = request.Login,Password=request.Password,RoleId=context.Roles.FirstOrDefault(u=>u.name=="user").id };
            context.Artists.Add(artist);
            context.SaveChangesAsync();
            return new RegistrationResponse { 
                Success = true,
                messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                    ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            };
        }
        [HttpPost]
        public async Task< RegistrationResponse> Login([FromBody]LoginViewModel login)
        {
            Console.WriteLine($"Received Login: {login.Login}, Role: {login.Role}, Password: {login.Password}");

            var user =context.Artists.FirstOrDefault(u=>u.Login==login.Login && u.Password==login.Password);
            if (user is null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    messages = new Dictionary<string, string[]>
                    {
                            { "Errors", new string[] { "Login or password is incorrect or user is not exist" } }
                    }
                };

            }
            if (login.Role.ToLower()=="admin")
            {
                var is_admin = context.Artists.FirstOrDefault(u => u.Login == user.Login && user.RoleId == 1);
                if (is_admin is null)
                {
                    return new RegistrationResponse
                    {
                        Success = false,
                        messages = new Dictionary<string, string[]>
                    {
                            { "Errors", new string[] { "You do not have an admin rights" } }
                    }
                    };
                }
            }
            if (!ModelState.IsValid)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                   ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                };
            }
           
            


            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name,login.Login),
               new Claim (ClaimTypes.Role,login.Role),


            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
            return new RegistrationResponse {
                Success = true,
                messages = ModelState.Where(x => x.Value.Errors.Count > 0).
                    ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()),
                cookies = claims.ToString()
            };



        }
        [HttpPost]
        public RegistrationResponse CheckRights([FromBody]LoginViewModel request)
        {
            var user = context.Artists.FirstOrDefault(u => u.Login == request.Login && u.RoleId == 1);
            if(user is null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    messages = new Dictionary<string, string[]>
                    {
                            { "Errors", new string[] { "You do not have an admin rights" } }
                    }
                };
            }
            return new RegistrationResponse
            {
                Success = true,
                messages = new Dictionary<string, string[]>
                    {
                            { "Messages", new string[] { "You are admin" } }
                    }
            };
        }
        [Authorize]
        [HttpGet]
        public  IActionResult GetUserRole()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            return Ok(new { role = user });
        }
        [HttpPost]
        public ActionResult GetName([FromBody]string name)
        {
            var user_name=context.Artists.FirstOrDefault(u=>u.Login==name);
            if(user_name is null)
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
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Ok(new { isAuthenticated = true, username = User.Identity.Name });
            }
            return Ok(new { isAuthenticated = false });
        }
    }
}
