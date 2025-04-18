using api_for_kursach.Repositories;
using api_for_kursach.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_for_kursach.DTOs;

namespace api_for_kursach.Services
{
    public interface IStudioService
    {
        Task<List<StudioDTO>> GetAllStudiosAsync();

    }
    public class StudiosService:IStudioService
    {
        private readonly IStudioRepository _studioRepository;

        // Конструктор, принимающий репозиторий
        public StudiosService(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        // Метод для получения всех студий
        public async Task<List<StudioDTO>> GetAllStudiosAsync()
        {
            return await _studioRepository.GetAllStudiosAsync();
        }
    }
}
