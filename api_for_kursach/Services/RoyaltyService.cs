using api_for_kursach.DTO;
using api_for_kursach.Repositories;

namespace api_for_kursach.Services
{
    public interface IRoyaltyService
    {
        Task<decimal> GetTotalMoney(ArtistDTO artist);
    }
    public class RoyaltyService : IRoyaltyService
    {
        private readonly IRoyaltiRepository _royal_rep;
        public RoyaltyService(IRoyaltiRepository royal_rep)
        {
            _royal_rep = royal_rep;
        }

        public async Task<decimal> GetTotalMoney(ArtistDTO artist)
        {
           return await  _royal_rep.GetTotalEarningsByArtist(artist.Id);
        }
    }
}
