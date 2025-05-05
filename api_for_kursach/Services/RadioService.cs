using api_for_kursach.DTO;

namespace api_for_kursach.Services
{
   public interface IRadioService
    {
        Task<List<RadioDTO>> GetRadioStations();
    }
    public class RadioService : IRadioService
    {
        private readonly IRadioRepository _radiorep;
        public RadioService(IRadioRepository radioRep)
        {
            _radiorep = radioRep;
        }
        public async  Task<List<RadioDTO>> GetRadioStations()
        {
            return await _radiorep.GetAllRadioStations();
        }
    }
}
