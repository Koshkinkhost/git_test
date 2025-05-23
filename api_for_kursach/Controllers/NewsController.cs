﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_for_kursach.ViewModels;
using api_for_kursach.Services;
namespace api_for_kursach.Controllers
{
    public class NewsController(INewsService newsService) : Controller
    {
        
        // GET: NewsController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            return Ok(await newsService.GetAllNews());
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
       
       
     

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
