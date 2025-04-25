using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace api_for_kursach.Repositories
{
    public interface ITrackRepository
    {
        Task<bool> UpdateTrackAsync(int id, TrackUpdatedDTO updatedTrack);
        Task<bool> DeleteTrackAsync(int id);
        Task CreateTrackByUserIdAsync(TrackViewModel track);

        Task<IEnumerable<TrackSimpleDTO>> GetAllTracksAsync();
        Task<TrackSimpleDTO> GetTrackByIdAsync(int id);
        Task<IEnumerable<TrackSimpleDTO>> GetTracksByAlbumIdAsync(string album);
        Task IncrementPlayCountAsync(int trackId);
        Task<IEnumerable<TrackSimpleDTO>> SearchTracksByTitleAsync(string title);
        Task<IEnumerable<TrackSimpleDTO>> GetTopTracksAsync(int topN);
        

    }
    public class TrackRepository : ITrackRepository
    {
        private readonly MusicLabelContext _context;
        public TrackRepository(MusicLabelContext context)
        {
            _context = context;
        }
        public async  Task<IEnumerable<TrackSimpleDTO>> GetAllTracksAsync()
        {
            return await _context.Tracks.Include(art=>art.Artist).Include(g=>g.Genre).Select(t=>new TrackSimpleDTO
            {
                TrackId = t.TrackId,
                Title = t.Title,
                Track_Artist=t.Artist.Name,
               Genre_track=t.Genre.GenreName,
               Listeners_count=t.PlaysCount,
               AlbumId=t.AlbumId,
              
            }).ToListAsync();
            
        }

        public async  Task<IEnumerable<TrackSimpleDTO>> GetTopTracksAsync(int topN)
        {
            return await _context.Tracks.Include(art => art.Artist).
                Include(g => g.Genre).OrderByDescending(t=>t.PlaysCount).Take(topN).
                Select(t => new TrackSimpleDTO
            {
                TrackId = t.TrackId,
                Title = t.Title,
                Track_Artist = t.Artist.Name,
                Genre_track = t.Genre.GenreName,
                Listeners_count= t.PlaysCount
            }).ToListAsync();

        }

        public async Task<TrackSimpleDTO> GetTrackByIdAsync(int id)
        {
            var result=await _context.Tracks.FirstOrDefaultAsync(t=>t.TrackId==id);
            if(result is not null)
           return  new TrackSimpleDTO() { TrackId = result.TrackId,Title=result.Title,Genre_track=result.Genre.GenreName };
            return null;
        }

        public async Task<IEnumerable<TrackSimpleDTO>> GetTracksByAlbumIdAsync(string album_title)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(al => al.Title == album_title);
            if (album != null)
            {
                return await _context.Tracks.Where(tr => tr.AlbumId == album.AlbumId).
                     Select(t => new TrackSimpleDTO
                     {
                         
                         TrackId = t.TrackId,
                         Track_Artist = t.Artist.Name,
                         Genre_track = t.Genre.GenreName
                     }
                     ).ToListAsync();
            }
       
            
            return new List<TrackSimpleDTO>();
        }

        public async Task IncrementPlayCountAsync(int trackId)
        {
            
            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == trackId);
            track.PlaysCount++;
            Console.Out.WriteLineAsync(track.PlaysCount.ToString());
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrackSimpleDTO>> SearchTracksByTitleAsync(string title)
        {
            return await _context.Tracks.Where(t=>t.Title.Contains(title)).
                Select(track=>new TrackSimpleDTO
                {
                    TrackId = track.TrackId,
                    Title=track.Title,
                    Track_Artist = track.Artist.Name,
                    Genre_track = track.Genre.GenreName,
                    Listeners_count=track.PlaysCount
                    
                }
                ).ToListAsync();
        }
        public async Task<bool> UpdateTrackAsync(int id,TrackUpdatedDTO dto)
        {
            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == dto.TrackId);
            if (track == null)
                return false;

            // Найти жанр по названию
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == dto.GenreName);
            if (genre == null)
                return false; // Можно вернуть ошибку или создать жанр, если надо

            // Обновить поля
            track.Title = dto.Title;
            track.GenreId = genre.GenreId;
            
            // Можно также обновить артиста, если нужно

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteTrackAsync(int id)
        {
            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == id);
            if (track == null)
                return false;

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateTrackByUserIdAsync(TrackViewModel track)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(r => r.GenreName == track.Genre_track);
           await  _context.Tracks.AddAsync(new Track() { ArtistId = track.ArtistId, Title = track.Title, GenreId = genre.GenreId });
            _context.SaveChangesAsync();
            
        }
    }
}
