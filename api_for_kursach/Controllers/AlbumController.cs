using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using api_for_kursach.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api_for_kursach.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly MusicLabelContext _musicLabelContext;
        
        public AlbumController(IAlbumService album,MusicLabelContext context) { _albumService = album;_musicLabelContext = context; }

        [HttpPost]
        public async Task<IActionResult> AlbumsWIthTracksByAlbumId([FromBody]AlbumDTO album)
        {

            return Ok(await _albumService.GetAlbumWithTracksAsync(album));
        }


        [HttpPost("with-tracks")]
        public async Task<IActionResult> AddAlbumWithTracks([FromForm] string albumData, [FromForm] List<IFormFile> audioFiles)
        {
            if (audioFiles == null || audioFiles.Count == 0)
            {
                return BadRequest("Файлы не выбраны.");
            }

            // Десериализуем метаданные альбома
            var albumDto = JsonConvert.DeserializeObject<TrackAlbumDTO>(albumData);

            // Создаем сущность альбома
            var album = new Album
            {
                Title = albumDto.Title,
                ReleaseDate = albumDto.ReleaseDate,
                ArtistId = albumDto.ArtistId,
                Tracks = new List<Track>()
            };

            // Обрабатываем каждый трек
            for (int i = 0; i < audioFiles.Count; i++)
            {
                var audioFile = audioFiles[i];
                var trackDto = albumDto.Tracks[i]; // предполагается, что количество треков и файлов совпадает

                if (audioFile.Length == 0)
                {
                    return BadRequest("Один из файлов пуст.");
                }

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
                    ArtistId = albumDto.ArtistId,
                    Title = trackDto.Title,
                   
                    GenreId = await GetGenreIdByName(trackDto.Genre_track),
                    AudioUrl = fileUrl,
                    PlaysCount = trackDto.Listeners_count,
                    Album = album // Связываем трек с альбомом
                };

                // Добавляем трек в коллекцию альбома
                album.Tracks.Add(track);
            }

            // Добавляем альбом в базу данных
            _musicLabelContext.Albums.Add(album);
            await _musicLabelContext.SaveChangesAsync();

            return Ok(new { album.AlbumId });
        }

        public async Task<int> GetGenreIdByName(string genreName)
        {
            var genre = await _musicLabelContext.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);
            if (genre == null)
            {
                throw new InvalidOperationException($"Жанр '{genreName}' не найден в базе данных.");
            }
            return genre.GenreId;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums(AlbumDTO album)
        { 
            return Ok(await _albumService.GetAllAlbumsAsync());
        }
        [HttpPost]
        public async Task<IActionResult> GetTracksALbum([FromBody]AlbumDTO album)
        {
            return Ok(await _albumService.GetTracksByAlbumIdAsync(album));
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
    public class AlbumUploadDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int PublisherId { get; set; }
        
    }
}
