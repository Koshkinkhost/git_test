
using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Services;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface IAristRepository
    {


        Task<IEnumerable<ArtistAlbumDTO>> GetArtistAlbumsAsync(ArtistDTO id); // Получить альбомы артиста
        Task<TrackDTO> GetArtistTracksByUserNameAsync(ArtistDTO username); // Получить треки артиста
        Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id); // Получить похожих артистов

    }
    public class ArtistRepository : IAristRepository
    {
        private readonly MusicLabelContext _context;
        public ArtistRepository(MusicLabelContext context) { _context = context; }
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


       

        public async Task<TrackDTO> GetArtistTracksByUserNameAsync(ArtistDTO userName)
        {
            // Находим пользователя по имени
            var user = await _context.Users
                .Where(u => u.Username == userName.name)
                .FirstOrDefaultAsync();

            // Если пользователь найден
            if (user != null)
            {
                // Найдем артиста, ассоциированного с этим пользователем
                var artist = await _context.Artists
                    .Where(a => a.UserId == user.UserId) // Предполагаем, что у вас есть связь UserId между пользователем и артистом
                    .FirstOrDefaultAsync();
                

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
                       }).ToListAsync();
                    return  new TrackDTO { Tracks =result};
                    // ; // Возвращаем список треков артиста
                }
               
            }
            return new TrackDTO { };

        }



        public Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
