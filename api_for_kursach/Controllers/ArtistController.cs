using api_for_kursach.DTO;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_for_kursach.Controllers
{
    
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        public ArtistController(IArtistService service)
        {
            artistService = service;
        }
        public async Task<IActionResult> GetAllArtists()
        {
            var r=await artistService.AllArtists();
            return Ok(r);

        }
        [HttpPost]
        public async  Task< IActionResult> GetTracksArtist([FromBody] ArtistDTO art)
        {
            await Console.Out.WriteLineAsync(art.name);
            try
            {
                return Ok(await artistService.GetArtistTracksAsync(art));

            }
            catch (Exception ex)
            {
                return Ok(ex.Message+ex.InnerException);
            }
            //   return Ok( await artistService.GetArtistTracksAsync(name));
        
        }
        [HttpPost]
        public async Task<IActionResult> GetAlbums([FromBody] ArtistDTO art)
        {
            await Console.Out.WriteLineAsync("name равно "+art.name);
            return Ok(await artistService.GetArtistAlbumsAsync(art));
        }
        // GET: ArtistController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ArtistController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtistController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistController/Create
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

        // GET: ArtistController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtistController/Edit/5
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

        // GET: ArtistController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistController/Delete/5
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
