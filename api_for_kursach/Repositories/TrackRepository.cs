using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface ITrackRepository
    {
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
               Genre_track=t.Genre.GenreName
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
                Genre_track = t.Genre.GenreName
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

        public Task<IEnumerable<TrackSimpleDTO>> SearchTracksByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

       
    }
}
