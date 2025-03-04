namespace api_for_kursach.middleware
{
    public class MiddleWareLog
    {
        private readonly   RequestDelegate next;
        private readonly ILogger<MiddleWareLog> _logger;
        public MiddleWareLog(RequestDelegate next, ILogger<MiddleWareLog> logger)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var originalStream = context.Request.Body;
            
            var reader= new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();
            await Console.Out.WriteLineAsync($"Запрос  " +
                $"Method: {context.Request.Method}" +
                $"| URL: {context.Request.Path}" +
                $"| Status Code: {context.Response.StatusCode}" +
                $"| Body {body}");

           
            context.Request.Body.Position = 0;
            
            context.Request.Body = originalStream;
            await next(context);
        }
    }
}
