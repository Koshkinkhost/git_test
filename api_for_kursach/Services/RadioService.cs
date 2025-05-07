using api_for_kursach.DTO;

namespace api_for_kursach.Services
{
    public interface IRadioService
    {
        Task<List<RadioDTO>> GetRadioStations();
        Task<List<RotationApplicationDTO>> GetRotationApplications();
        Task<int> AddRotationAppilcation(RotationApplicationDTO rotationApplicationDTO);

        // ✅ Новый метод
        Task<bool> UpdateRotationStatus(int applicationId, string newStatus);
    }

    public class RadioService : IRadioService
    {
        private readonly IRadioRepository _radiorep;

        public RadioService(IRadioRepository radioRep)
        {
            _radiorep = radioRep;
        }

        public async Task<int> AddRotationAppilcation(RotationApplicationDTO rotationApplicationDTO)
        {
            return await _radiorep.CreateRotationApplication(rotationApplicationDTO);
        }

        public async Task<List<RadioDTO>> GetRadioStations()
        {
            return await _radiorep.GetAllRadioStations();
        }

        public async Task<List<RotationApplicationDTO>> GetRotationApplications()
        {
            return await _radiorep.GetAllRotationApplications();
        }

        // ✅ Реализация метода обновления статуса
        public async Task<bool> UpdateRotationStatus(int applicationId, string newStatus)
        {
            return await _radiorep.UpdateRotationStatus(applicationId, newStatus);
        }
    }
}
