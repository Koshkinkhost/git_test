namespace api_for_kursach.ViewModels
{
    public class NewsViewModel
    {
        public string Title { get; set; } // Заголовок новости
        public string Description { get; set; } // Описание новости
        public string ImageUrl { get; set; } // Путь к изображению
        public string Link { get; set; } // Ссылка на страницу новости
        public DateTime Date { get; set; } // Дата публикации новости

        // Можно добавить дополнительные свойства, если нужно (например, для фильтрации или сортировки)
    }
}
