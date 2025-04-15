using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_for_kursach.Services
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(AlbumDTO album);
        Task AddAlbumAsync(Album album);
        Task DeleteAlbumAsync(int id);
    }

    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _albumRepository.GetAllAlbumsAsync();
        }

        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _albumRepository.GetAlbumByIdAsync(id);
        }

        public async Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(AlbumDTO album)
        {
            return await _albumRepository.GetAlbumWithTracksAsync(album.Id);
        }

        public async Task AddAlbumAsync(Album album)
        {
            await _albumRepository.AddAlbumAsync(album);
        }

        public async Task DeleteAlbumAsync(int id)
        {
            await _albumRepository.DeleteAlbumAsync(id);
        }

        
    }
}
