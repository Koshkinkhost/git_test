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

       
    }

   
}