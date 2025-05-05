using api_for_kursach.Repositories;
using api_for_kursach.Models;
using api_for_kursach.DTO;
namespace api_for_kursach.Services
{
    public interface IArtistService
    {
        Task<Artist> GetArtistByIdAsync(int id); // Получить артиста по ID

        Task<List<ArtistDTO>> AllArtists();
        Task<IEnumerable<ArtistAlbumDTO>> GetArtistAlbumsAsync(ArtistDTO id); // Получить альбомы артиста
        Task<TracksDTO> GetArtistTracksAsync(ArtistDTO artist); // Получить треки артиста
        Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id); // Получить похожих артистов
        Task<List<RotationApplicationDTO>> GetRotationApplicationsByArtistId(ArtistDTO artist);
    }

    public class ArtistService : IArtistService
    {
        private readonly IAristRepository _artistRep;
        public ArtistService(IAristRepository artistRespository)
        {
            _artistRep = artistRespository;
        }

        public async Task<List<ArtistDTO>> AllArtists()
        {
            return await _artistRep.GetAll();
        }

        public async Task<IEnumerable<ArtistAlbumDTO>> GetArtistAlbumsAsync(ArtistDTO art)
        {
            return await  _artistRep.GetArtistAlbumsAsync(art);

        }

        public Task<Artist> GetArtistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TracksDTO> GetArtistTracksAsync(ArtistDTO artist)
        {
            if (artist.Id > 0 && artist is not null)
            {
                return await _artistRep.GetArtistTracksByUserIdAsync(artist.Id);
            }
            return new TracksDTO();
        }

        public Task<List<RotationApplicationDTO>> GetRotationApplicationsByArtistId(ArtistDTO artist)
        {
            return _artistRep.GetRotationApplicationsByArtistId(artist.Id);
        }

        public Task<IEnumerable<Artist>> GetSimilarArtistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
