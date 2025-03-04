using api_for_kursach;
using api_for_kursach.middleware;
using api_for_kursach.Models;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddUserService();
builder.Services.AddFactoryService();
builder.Services.AddDbContext<AppliContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
        options.Cookie.Name = "AuthCookie";

    });
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<MiddleWareLog>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();
