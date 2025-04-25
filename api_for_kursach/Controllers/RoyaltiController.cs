using api_for_kursach.DTO;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_for_kursach.Controllers
{
    public class RoyaltiController : Controller
    {
        private readonly IRoyaltyService _royaltyService;
        public RoyaltiController(IRoyaltyService royaltyService)
        {
            _royaltyService = royaltyService;
        }
        // GET: RoyaltiController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetTotalMoney([FromBody]ArtistDTO artist)
        {
            return Ok(await _royaltyService.GetTotalMoney(artist));
        }
        [HttpPost]
        public async Task<IActionResult> GetTracksRoyalti([FromBody]ArtistDTO artist)
        {
            return Ok(await _royaltyService.GetTrackMoney(artist));
        }
        // GET: RoyaltiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoyaltiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoyaltiController/Create
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

        // GET: RoyaltiController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoyaltiController/Edit/5
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

        // GET: RoyaltiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoyaltiController/Delete/5
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
