
using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Services;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface IAristRepository
    {

        Task<List<ArtistDTO>> GetAll();
        Task<IEnumerable<ArtistAlbumDTO>> GetArtistAlbumsAsync(ArtistDTO id); // Получить альбомы артиста
        Task<TracksDTO> GetArtistTracksByUserIdAsync(int id); // Получить треки артиста
        Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id); // Получить похожих артистов

    }
    public class ArtistRepository : IAristRepository
    {
        private readonly MusicLabelContext _context;
        public ArtistRepository(MusicLabelContext context) { _context = context; }

        public async Task<List<ArtistDTO>> GetAll()
        {
            return await _context.Artists.Select(t => new ArtistDTO
            {
                Id = t.ArtistId,
                name = t.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<ArtistAlbumDTO>> GetArtistAlbumsAsync(ArtistDTO art)
        {
            await Console.Out.WriteLineAsync(art.name.ToString());
            var user=_context.Users.FirstOrDefault(u=>u.Username==art.name);
            if(user is not null)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if (artist is not null)
                {
                    var result = await _context.Albums
             .Where(album => album.ArtistId == artist.ArtistId)
             .Select(album => new ArtistAlbumDTO
             {
                 ArtistId = album.Artist.ArtistId,
                 Artist = album.Artist.Name,
                 Title = album.Title,
                 RealeseDate = album.ReleaseDate,
             })

             .ToListAsync();
                    await Console.Out.WriteLineAsync(result.ToString());
                    return result;
                }



           
            }
            return null;


           
        }


       

        public async Task<TracksDTO> GetArtistTracksByUserIdAsync(int id)
        {
            // Находим артиста по айди
            var artist = await _context.Artists
                .Where(u => u.ArtistId == id)
                .FirstOrDefaultAsync();

            // Если пользователь найден
           
                

                // Если артист найден, получаем его треки
                if (artist != null)
                {
                    var result = await _context.Tracks
                       .Where(t => t.ArtistId == artist.ArtistId)
                       .Select(t => new TrackSimpleDTO
                       {
                           TrackId = t.TrackId,
                           Title = t.Title,
                           Track_Artist=t.Artist.Name,
                           Genre_track=t.Genre.GenreName,
                           Listeners_count=t.PlaysCount
                           
                       }).ToListAsync();
                    return  new TracksDTO { Tracks =result};
                    // ; // Возвращаем список треков артиста
                }
               
            
            return new TracksDTO { };

        }



        public Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
