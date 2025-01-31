using System.ComponentModel.DataAnnotations;

namespace api_for_kursach.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }                   // Уникальный идентификатор
        public string Title { get; set; }             // Заголовок новости
        public string Description { get; set; }       // Описание новости
        public string ImageUrl { get; set; }          // Относительный путь к изображению
        public string Link { get; set; }              // Относительный путь на страницу новости
        public DateTime CreatedAt { get; set; }       // Дата создания
    }
}
