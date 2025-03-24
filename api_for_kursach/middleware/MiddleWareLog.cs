namespace api_for_kursach.middleware
{
    public class MiddleWareLog
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddleWareLog> _logger; // Логгер для записи логов

        public MiddleWareLog(RequestDelegate next, ILogger<MiddleWareLog> logger)
        {
            this.next = next;
            _logger = logger; // Инициализация логгера
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Включаем буферизацию запроса, чтобы можно было несколько раз читать тело
            context.Request.EnableBuffering();

            // Копируем оригинальный поток тела запроса
            var originalStream = context.Request.Body;
            var memoryStream = new MemoryStream();

            // Копируем данные из оригинального потока в новый memoryStream
            await context.Request.Body.CopyToAsync(memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            // Читаем тело запроса для логирования
            var reader = new StreamReader(memoryStream);
            string body = await reader.ReadToEndAsync();

            // Логируем запрос с использованием ILogger
            _logger.LogInformation($"Запрос  " +
                $"Method: {context.Request.Method}" +
                $"| URL: {context.Request.Path}" +
                $"| Status Code: {context.Response.StatusCode}" +
                $"| Body {body}");

            // Возвращаем позицию в начало в originalStream, чтобы последующие компоненты могли его использовать
            memoryStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = memoryStream;

            // Передаем запрос дальше по конвейеру
            await next(context);
        }
    }
}
