using api_for_kursach.DTOs;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface IStudioRepository
    {
        Task<List<StudioDTO>> GetAllStudiosAsync();


    }

    public class StudioRepository:IStudioRepository
        {
            private readonly MusicLabelContext _context;

            // Конструктор, принимающий контекст базы данных
            public StudioRepository(MusicLabelContext context)
            {
                _context = context;
            }

            // Метод для получения всех студий
            public async Task<List<StudioDTO>> GetAllStudiosAsync()
            {
           
                var result= await  _context.Studios.ToListAsync();
                    var  DTOs=result.Select(studio=>new StudioDTO
                    {
                        StudioId = studio.StudioId,
                        Name = studio.Name,
                        Location = studio.Location,
                        FoundedYear = studio.FoundedYear,
                        Phone = studio.Phone,
                        Email = studio.Email,
                        City = studio.City,
                        Street = studio.Street,
                        Building = studio.Building,
                        Latitude = studio.Latitude,
                        Longitude = studio.Longitude
                    }).ToList();
            return DTOs;
            }
        }
    }


