using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface IRoyaltiRepository
    {
        Task<decimal> GetTotalEarningsByArtist(int artistId);
        Task<Dictionary<string, decimal>> GetEarningsByTrackForArtist(int artistId);
    }
    public class RoyaltiRepository : IRoyaltiRepository
    {
        private readonly MusicLabelContext _musicLabelContext;
        public RoyaltiRepository(MusicLabelContext musicLabelContext)
        {
            _musicLabelContext= musicLabelContext;
        }
        public async Task<Dictionary<string, decimal>> GetEarningsByTrackForArtist(int artistId)
        {
           return await _musicLabelContext.Tracks
        .Where(t => t.ArtistId == artistId)
        .Select(t => new
        {
            t.Title,
            total = t.PlaysCount * t.Royalties.Sum(r => r.Amount)
        })
        .ToDictionaryAsync(x => x.Title, x => x.total);
        }

        public async Task<decimal> GetTotalEarningsByArtist(int artistId)
        {
            var total = await (from royalty in _musicLabelContext.Royalties 
                        join tracks in _musicLabelContext.Tracks on
                        royalty.TrackId equals tracks.TrackId
                        where royalty.AuthorId==artistId
                        select tracks.PlaysCount*royalty.Amount).SumAsync();
            return total;
        }
    }
}
