using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_for_kursach.ViewModels;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
namespace api_for_kursach.Controllers
{
    public class StudiosController() : Controller
    {
        [HttpPost]
        //public async Task<IActionResult> AddStudio([FromBody] StudioViewModel studio)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Studio s = new Studio()
        //        {
        //            latitude = studio.lat,
        //            longitude = studio.longt,
        //            name = studio.name,
        //            phone_num = studio.phone_num,
        //            email = studio.email,
        //            street = studio.street,
        //            city=studio.city,
        //            build = studio.build
        //        };
        //        context.Add(s);
        //        await context.SaveChangesAsync();


        //        return Ok(); 
        //    }

        //    return BadRequest("что то пошло не так"); 
        //}
        //public async  Task<IActionResult> Get_Studios()
        //{
        //    var result= await context.studios.ToListAsync();
        //    return Json(result);
        //}

        // GET: StudiosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudiosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudiosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudiosController/Create
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

        // GET: StudiosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudiosController/Edit/5
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

        // GET: StudiosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudiosController/Delete/5
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
    }
}
