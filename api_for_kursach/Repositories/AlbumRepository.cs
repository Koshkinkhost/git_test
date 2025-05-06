using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api_for_kursach.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<AlbumDTO_find>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(int id);
        Task AddAlbumAsync(AlbumDTO album);
        Task DeleteAlbumAsync(int id);
    }

    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicLabelContext _context;

        public AlbumRepository(MusicLabelContext context)
        {
            _context = context;
        }

        public async Task<List<AlbumDTO_find>> GetAllAlbumsAsync()
        {
            return await _context.Albums
                .Include(a => a.Artist) // Включаем информацию о артисте
                .Include(a => a.Tracks)  // Включаем треки, чтобы посчитать общее количество прослушиваний
                .Select(a => new AlbumDTO_find
                {
                    AlbumId = a.AlbumId,
                    ArtistId = a.ArtistId,
                    ArtistName = a.Artist.Name,
                    Album_title=a.Title,
                    genre_name=a.Tracks.FirstOrDefault().Genre.GenreName,
                    TotalPlays = a.Tracks.Sum(t => t.PlaysCount) // Суммируем количество прослушиваний всех треков альбома
                })
                .ToListAsync();
        }


        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _context.Albums
                .FirstOrDefaultAsync(a => a.AlbumId == id);
        }

        public async Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(int id)
        {
            return await _context.Albums
                .Where(a => a.AlbumId == id)
                .Select(a => new AlbumTracksDTO
                {
                    Title = a.Title,
                    Tracks = a.Tracks.Select(t => t.Title).ToList()
                })
                .ToListAsync();
        }


        public async Task AddAlbumAsync(AlbumDTO album)
        {
            // Проверяем, что объект альбома не null
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album), "Альбом не может быть null.");
            }

            // Создаем новую сущность альбома и маппим данные из DTO
            var newAlbum = new Album
            {
                ArtistId = album.ArtistId,
                Title = album.Name,
                ReleaseDate = DateOnly.FromDateTime(DateTime.UtcNow), // Устанавливаем текущую дату
                Tracks = new List<Track>() // Инициализируем коллекцию треков
            };

            // Если есть треки, добавляем их в альбом
            if (album.Tracks != null && album.Tracks.Any())
            {
                foreach (var trackDto in album.Tracks)
                {
                    // Находим GenreId по названию жанра
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == trackDto.Genre_track);
                    if (genre == null)
                    {
                        throw new InvalidOperationException($"Жанр '{trackDto.Genre_track}' не найден в базе данных.");
                    }

                    // Создаем новый трек
                    var track = new Track
                    {
                        TrackId = trackDto.TrackId,
                        ArtistId = trackDto.ArtistId,
                        Title = trackDto.Title, // Оставляем оригинальное название трека
                        AlbumId = newAlbum.AlbumId, // Устанавливаем связь с альбомом
                        GenreId = genre.GenreId, // Устанавливаем GenreId из найденного жанра
                        PlaysCount = trackDto.Listeners_count
                    };

                    newAlbum.Tracks.Add(track); // Добавляем трек в коллекцию альбома
                }
            }

            // Добавляем альбом в контекст и сохраняем изменения
            _context.Albums.Add(newAlbum);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAlbumAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
        }
    }
}
