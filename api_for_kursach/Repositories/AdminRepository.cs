using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_for_kursach.Repositories
{
    public interface IAdminRepository
    {

        Task<IEnumerable<ArtistIncomeStat>> GetAllArtistsWithIncomeAsync();
        Task<bool> DecreaseRoyaltyAsync(int artistId, decimal amount);
        Task<IEnumerable<RotationApplication>> GetAllRotationApplicationsAsync();
        Task<bool> ApproveRotationApplicationAsync(int applicationId);
        Task<bool> DeleteAlbumWithTracksAndRotationsAsync(int albumId);
        Task<bool> RejectRotationApplicationAsync(int applicationId);
        Task<IEnumerable<ArtistIncomeStat>> GetArtistIncomeStatisticsAsync();
        Task<IEnumerable<RotationStat>> GetRotationStatisticsAsync();
        Task<bool> DeleteArtistAsync(int artistId);
    }
    public class AdminRepository : IAdminRepository
    {
        private readonly MusicLabelContext _context;

        public AdminRepository(MusicLabelContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteArtistAsync(int artistId)
        {
            var artist = await _context.Artists
                .Include(a => a.Tracks)
                    .ThenInclude(t => t.RotationApplications)
                .Include(a => a.Tracks)
                    .ThenInclude(t => t.Royalties)
                .Include(a => a.Albums)
                .FirstOrDefaultAsync(a => a.ArtistId == artistId);

            if (artist == null)
                return false;

            // Удаляем связанные заявки на ротации и роялти по каждому треку
            foreach (var track in artist.Tracks)
            {
                _context.RotationApplications.RemoveRange(track.RotationApplications);
                _context.Royalties.RemoveRange(track.Royalties);
            }

            // Удаляем треки артиста
            _context.Tracks.RemoveRange(artist.Tracks);

            // Удаляем альбомы артиста
            _context.Albums.RemoveRange(artist.Albums);

            // Удаляем самого артиста
            _context.Artists.Remove(artist);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlbumWithTracksAndRotationsAsync(int albumId)
        {
            var album = await _context.Albums
                .Include(a => a.Tracks)
                    .ThenInclude(t => t.RotationApplications)
                .Include(a => a.Tracks)
                    .ThenInclude(t => t.Royalties)
                .FirstOrDefaultAsync(a => a.AlbumId == albumId);

            if (album == null)
                return false;

            foreach (var track in album.Tracks)
            {
                // Удаляем заявки на ротацию
                _context.RotationApplications.RemoveRange(track.RotationApplications);

                // Удаляем роялти по треку
                _context.Royalties.RemoveRange(track.Royalties);
            }

            // Удаляем треки альбома
            _context.Tracks.RemoveRange(album.Tracks);

            // Удаляем сам альбом
            _context.Albums.Remove(album);

            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<IEnumerable<ArtistIncomeStat>> GetAllArtistsWithIncomeAsync()
        {
            return await _context.Artists.Select(a => new ArtistIncomeStat
            {
                ArtistId = a.ArtistId,
                ArtistName = a.Name,
                TotalIncome = a.Royalties.Sum(r => r.Amount)
            }).ToListAsync();
        }

       

        public async Task<bool> DecreaseRoyaltyAsync(int artistId, decimal amount)
        {
            var royalties = await _context.Royalties.Where(r => r.TrackId == artistId).ToListAsync();
            foreach (var royalty in royalties)
            {
                royalty.Amount -= amount;
                _context.Royalties.Update(royalty);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RotationApplication>> GetAllRotationApplicationsAsync()
        {
            return await _context.RotationApplications.Include(r => r.Track).ToListAsync();
        }

        public async Task<bool> ApproveRotationApplicationAsync(int applicationId)
        {
            var application = await _context.RotationApplications.FindAsync(applicationId);
            if (application == null) return false;

            application.Status = "Approved";
            _context.RotationApplications.Update(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectRotationApplicationAsync(int applicationId)
        {
            var application = await _context.RotationApplications.FindAsync(applicationId);
            if (application == null) return false;

            application.Status = "Rejected";
            _context.RotationApplications.Update(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ArtistIncomeStat>> GetArtistIncomeStatisticsAsync()
        {
            return await _context.Artists.Select(a => new ArtistIncomeStat
            {
                ArtistId = a.ArtistId,
                ArtistName = a.Name,
                TotalIncome = a.Royalties.Sum(r => r.Amount)
            }).ToListAsync();
        }

        public async Task<IEnumerable<RotationStat>> GetRotationStatisticsAsync()
        {
            return await _context.RotationApplications.Select(r => new RotationStat
            {
                TrackTitle = r.Track.Title,
                ArtistName = r.Track.Artist.Name,
               
            }).ToListAsync();
        }
    }
    public class ArtistIncomeStat
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public decimal TotalIncome { get; set; }
    }

    public class RotationStat
    {
        public string TrackTitle { get; set; }
        public string ArtistName { get; set; }
        public int RotationCount { get; set; }
    }

    public enum RotationStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
