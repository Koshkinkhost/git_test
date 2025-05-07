using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Services
{
    public interface IRadioRepository
    {
        Task<List<RadioDTO>> GetAllRadioStations();
        Task<List<RotationApplicationDTO>> GetAllRotationApplications();
        Task<int> CreateRotationApplication(RotationApplicationDTO application);
        Task<bool> UpdateRotationStatus(int applicationId, string newStatus);


    }

    public class RadioRepository : IRadioRepository
    {
        private readonly MusicLabelContext _context;

        public RadioRepository(MusicLabelContext context)
        {
            _context = context;
        }

        public async Task<List<RadioDTO>> GetAllRadioStations()
        {
            var radioStations = await _context.RadioStations
                .Select(r => new RadioDTO
                {
                    RadioStationId = r.RadioStationId,
                    Name = r.Name,
                    Frequency = r.Frequency,
                    Country = r.Country
                })
                .ToListAsync();

            return radioStations;
        }
        public async Task<int> CreateRotationApplication(RotationApplicationDTO application)
        {
            // Создаем новую сущность заявки на ротацию
            var rotationApplication = new RotationApplication
            {
                TrackId = application.TrackId,
                RadioStationId = application.RadioStationId,
                Status = application.Status,
                ApplicationDate = DateTime.UtcNow, // Устанавливаем текущую дату подачи заявки
                ReviewDate = null, // Дата проверки может быть установлена позже
                Notes = application.Notes
            };  

            // Добавляем заявку в контекст базы данных
            _context.RotationApplications.Add(rotationApplication);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            // Возвращаем ID созданной заявки
            return rotationApplication.ApplicationId;
        }

        public Task<List<RotationApplicationDTO>> GetAllRotationApplications()
        {
            return _context.RotationApplications.Select(u=>new RotationApplicationDTO
            {
                TrackId=u.TrackId,
                TrackTitle=u.Track.Title,
                ApplicationId=u.ApplicationId,
                Notes=u.Notes,
                ArtistName=u.Track.Artist.Name,
                RadioStationName=u.RadioStation.Name,
                RadioStationId=u.RadioStationId,
                Status = u.Status,
                ApplicationDate=u.ApplicationDate
            }).ToListAsync();
        }

        public async Task<bool> UpdateRotationStatus(int applicationId, string newStatus)
        {
            var application = await _context.RotationApplications.FindAsync(applicationId);
            if (application == null) return false;

            application.Status = newStatus.ToLower(); // Приводим к нижнему регистру, если нужно
            application.ReviewDate = DateTime.UtcNow; // можно также обновлять дату рассмотрения

            await _context.SaveChangesAsync();
            return true;
        }
    }

   
}