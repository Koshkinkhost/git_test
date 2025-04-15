using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api_for_kursach.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(int id);
        Task AddAlbumAsync(Album album);
        Task DeleteAlbumAsync(int id);
    }

    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicLabelContext _context;

        public AlbumRepository(MusicLabelContext context)
        {
            _context = context;
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _context.Albums
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


        public async Task AddAlbumAsync(Album album)
        {
            _context.Albums.Add(album);
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
