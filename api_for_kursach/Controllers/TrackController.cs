using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_for_kursach.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTracks()
        {
            
            return Ok(await  _trackService.GetAllTracksAsync());
        }
        [HttpPost]
        public async Task<IActionResult> GetTracksByAlbum([FromBody]AlbumDTO album)
        {

            return Ok(await _trackService.GetTracksByAlbumIdAsync(album));
        }
        [HttpPost]
        public async Task<IActionResult> IncrementListens([FromBody]TrackSimpleDTO track)
        {
            await _trackService.IncrementPlayCountAsync(track.TrackId);
            return Ok();
        }
        // GET: TrackController

        public ActionResult Index()
        {
            return View();
        }

        // GET: TrackController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrackController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrackController/Create
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

        // GET: TrackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrackController/Edit/5
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

        // GET: TrackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrackController/Delete/5
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
