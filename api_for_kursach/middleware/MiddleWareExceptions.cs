namespace api_for_kursach.middleware
{
    public class MiddleWareExceptions
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddleWareLog> _logger; // Логгер для записи логов

        public MiddleWareExceptions(RequestDelegate next, ILogger<MiddleWareLog> logger)
        {
            this.next = next;
            _logger = logger; // Инициализация логгера
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                
                _logger.LogInformation(ex.Message);
            }
        }

    }
}
