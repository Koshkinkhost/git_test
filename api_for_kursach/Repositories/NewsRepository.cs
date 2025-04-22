using api_for_kursach.DTO;
using api_for_kursach.Models;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Repositories
{
    public interface INewsRepository
    {
        // Метод для получения списка новостей
        Task<List<NewsDto>> GetAllNews();
    }
    public class NewsRepository : INewsRepository
    {
        private readonly MusicLabelContext _context;

        // Конструктор, принимающий контекст базы данных
        public NewsRepository(MusicLabelContext context)
        {
            _context = context;
        }

        // Метод для получения списка всех новостей
        public async Task<List<NewsDto>> GetAllNews()
        {
            return await _context.News.Select(
                t=>new NewsDto 
                { Title=t.Title,
                    Content=t.Content

                }
                
                ).ToListAsync();
        }
    }
}
