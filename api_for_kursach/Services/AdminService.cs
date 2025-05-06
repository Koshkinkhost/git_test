using api_for_kursach.Models;
using api_for_kursach.Repositories;

namespace api_for_kursach.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<ArtistIncomeStat>> GetAllArtistsWithIncomeAsync();
        Task<bool> DecreaseRoyaltyAsync(int artistId, decimal amount);
        Task<IEnumerable<RotationApplication>> GetAllRotationApplicationsAsync();
        Task<bool> ApproveRotationApplicationAsync(int applicationId);
        Task<bool> RejectRotationApplicationAsync(int applicationId);
        Task<bool> DeleteAlbumWithTracksAndRotationsAsync(int albumId);
        Task<bool> DeleteArtistAsync(int artistId);
        Task<IEnumerable<ArtistIncomeStat>> GetArtistIncomeStatisticsAsync();
        Task<IEnumerable<RotationStat>> GetRotationStatisticsAsync();
    }
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<ArtistIncomeStat>> GetAllArtistsWithIncomeAsync()
        {
            return await _adminRepository.GetAllArtistsWithIncomeAsync();
        }

        public async Task<bool> DecreaseRoyaltyAsync(int artistId, decimal amount)
        {
            return await _adminRepository.DecreaseRoyaltyAsync(artistId, amount);
        }

        public async Task<IEnumerable<RotationApplication>> GetAllRotationApplicationsAsync()
        {
            return await _adminRepository.GetAllRotationApplicationsAsync();
        }

        public async Task<bool> ApproveRotationApplicationAsync(int applicationId)
        {
            return await _adminRepository.ApproveRotationApplicationAsync(applicationId);
        }

        public async Task<bool> RejectRotationApplicationAsync(int applicationId)
        {
            return await _adminRepository.RejectRotationApplicationAsync(applicationId);
        }

        public async Task<bool> DeleteAlbumWithTracksAndRotationsAsync(int albumId)
        {
            return await _adminRepository.DeleteAlbumWithTracksAndRotationsAsync(albumId);
        }

        public async Task<bool> DeleteArtistAsync(int artistId)
        {
            return await _adminRepository.DeleteArtistAsync(artistId);
        }

        public async Task<IEnumerable<ArtistIncomeStat>> GetArtistIncomeStatisticsAsync()
        {
            return await _adminRepository.GetArtistIncomeStatisticsAsync();
        }

        public async Task<IEnumerable<RotationStat>> GetRotationStatisticsAsync()
        {
            return await _adminRepository.GetRotationStatisticsAsync();
        }
    }
}
