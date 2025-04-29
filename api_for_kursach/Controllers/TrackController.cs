using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Services;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_for_kursach.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly MusicLabelContext _context;
        public TrackController(ITrackService trackService,MusicLabelContext context)
        {
            _trackService = trackService;
            _context = context;
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
        [HttpPost]
        public async Task<IActionResult> GetTracksByTitle([FromBody]TrackSimpleDTO track)
        {
            try
            {
                return Ok(await _trackService.SearchTracksByTitleAsync(track));

            }
            catch(InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }



        }
        [HttpPost]
        public async Task<IActionResult> UpdateTrack([FromBody]TrackUpdatedDTO track)
        {
            return Ok(await _trackService.UpdateTrack(track));
        }
        [HttpPost]
        public async Task<IActionResult> UploadTrack([FromForm] string trackData, [FromForm] IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return BadRequest("Файл не выбран.");
            }

            // Десериализуем метаданные трека
            var trackDto = JsonConvert.DeserializeObject<TrackUploadDTO>(trackData);

            // Генерируем уникальное имя файла
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(audioFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio", fileName);

            // Сохраняем файл
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await audioFile.CopyToAsync(stream);
            }

            // Формируем URL для доступа к файлу
            var fileUrl = $"/audio/{fileName}";

            // Создаем сущность трека
            var track = new Track
            {
                ArtistId = trackDto.ArtistId,
                Title = trackDto.Title,
                GenreId = await GetGenreIdByName(trackDto.genreTrack),
                PlaysCount = trackDto.Listeners_count,
                AudioUrl = fileUrl,
            };

            // Добавляем трек в базу данных
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Трек успешно загружен.", url = fileUrl });
        }
        public async Task<int> GetGenreIdByName(string genreName)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);
            if (genre == null)
            {
                throw new InvalidOperationException($"Жанр '{genreName}' не найден в базе данных.");
            }
            return genre.GenreId;
        }
        // GET: TrackController
        [HttpGet]
        public async Task<IActionResult> GetTopTracks(int n)
        {
            return Ok(await _trackService.GetTopTracksAsync(n));
        }
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
