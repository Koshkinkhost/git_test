using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_for_kursach.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        
        public AlbumController(IAlbumService album) { _albumService = album; }

        [HttpPost]
        public async Task<IActionResult> AlbumsWIthTracksByAlbumId([FromBody]AlbumDTO album)
        {

            return Ok(await _albumService.GetAlbumWithTracksAsync(album));
        }


        [HttpPost("upload")]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumDTO album)
        {
            try
            {
                await _albumService.AddAlbumAsync(album);

            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Success");    
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAlbums(AlbumDTO album)
        { 
            return Ok(await _albumService.GetAllAlbumsAsync());
        }

        // GET: AlbumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlbumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlbumController/Create
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

        // GET: AlbumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AlbumController/Edit/5
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

        // GET: AlbumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlbumController/Delete/5
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
