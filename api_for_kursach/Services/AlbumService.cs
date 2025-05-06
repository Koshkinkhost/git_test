using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_for_kursach.Services
{
    public interface IAlbumService
    {
        Task<List<AlbumDTO_find>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<List<AlbumTracksDTO?>> GetAlbumWithTracksAsync(AlbumDTO album);
        Task AddAlbumAsync(AlbumDTO album);
        Task DeleteAlbumAsync(int id);
    }

    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<List<AlbumDTO_find>> GetAllAlbumsAsync()
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

        public async Task AddAlbumAsync(AlbumDTO album)
        {
            if(album.Id == 0 || album.ArtistId==0 || String.IsNullOrEmpty(album.Name))
            {
                throw new ArgumentException("Не все данные переданы");
            }
            await _albumRepository.AddAlbumAsync(album);
        }

        public async Task DeleteAlbumAsync(int id)
        {
            await _albumRepository.DeleteAlbumAsync(id);
        }

        
    }
}
