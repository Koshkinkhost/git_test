using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using System;
using System.Collections.Generic;

namespace api_for_kursach.Services
{
    public interface INewsService
    {
        Task<List<NewsDto>> GetAllNews();
        Task<NewsDto> GetNewsById(int newsId);
        void CreateNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int newsId);
    }
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        // Конструктор, принимающий репозиторий новостей
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // Метод для получения всех новостей
        public async Task<List<NewsDto>> GetAllNews()
        {
            return await _newsRepository.GetAllNews();
        }

        // Метод для получения новости по ID
        public async Task <NewsDto> GetNewsById(int newsId)
        {
            var news = _newsRepository.GetAllNews();
            if (news == null)
            {
                throw new ArgumentException("News not found.");
            }
            return new NewsDto();
        }

        // Метод для создания новости
        public void CreateNews(News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news), "News cannot be null.");
            }

        }

        // Метод для обновления новости
        public void UpdateNews(News news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news), "News cannot be null.");
            }

            var existingNews = _newsRepository.GetAllNews();
            if (existingNews == null)
            {
                throw new ArgumentException("News not found.");
            }

        }

        // Метод для удаления новости
        public void DeleteNews(int newsId)
        {
            var existingNews = _newsRepository.GetAllNews();
            if (existingNews == null)
            {
                throw new ArgumentException("News not found.");
            }

        }
    }
}
