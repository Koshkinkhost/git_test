using api_for_kursach.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_for_kursach.ViewModels;
namespace api_for_kursach.Controllers
{
    public class NewsController(AppliContext context) : Controller
    {
        // GET: NewsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetNews()
        {
            var news = context.news;
            return Json(news);
        }
        [HttpPost]
        public void AddNews([FromBody]NewsViewModel model)
        {
            
            var news = new News() { Title=model.Title,Description=model.Description,ImageUrl=model.ImageUrl,Link=model.Link, CreatedAt = model.Date};
            context.news.Add(news);
            context.SaveChanges();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
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

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsController/Edit/5
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

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsController/Delete/5
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
