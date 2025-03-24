﻿namespace api_for_kursach.middleware
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
            try
            {
                context.Request.EnableBuffering();
                var originalStream = context.Request.Body;
                var memoryStream = new MemoryStream();
                await context.Request.Body.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var reader = new StreamReader(memoryStream);
                string body = await reader.ReadToEndAsync();

                _logger.LogInformation($"Request Method: {context.Request.Method}, Body: {body}");

                memoryStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = memoryStream;

                await next(context);  // Передаем запрос дальше по конвейеру
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing request: {ex.Message}");
                context.Response.StatusCode = 500;  // Внутренняя ошибка сервера
                await context.Response.WriteAsync("Internal Server Error");
            }
        }

    }
}
