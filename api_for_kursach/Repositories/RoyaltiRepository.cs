using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface IRoyaltiRepository
    {
        Task<decimal> GetTotalEarningsByArtist(int artistId);
        Dictionary<int, decimal> GetEarningsByTrackForArtist(int artistId);
    }
    public class RoyaltiRepository : IRoyaltiRepository
    {
        private readonly MusicLabelContext _musicLabelContext;
        public RoyaltiRepository(MusicLabelContext musicLabelContext)
        {
            _musicLabelContext= musicLabelContext;
        }
        public Dictionary<int, decimal> GetEarningsByTrackForArtist(int artistId)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTotalEarningsByArtist(int artistId)
        {
            var total = await (from royalty in _musicLabelContext.Royalties 
                        join tracks in _musicLabelContext.Tracks on
                        royalty.TrackId equals tracks.TrackId
                        where royalty.AuthorId==tracks.ArtistId
                        select tracks.PlaysCount*royalty.Amount).SumAsync();
            return total;
        }
    }
}
