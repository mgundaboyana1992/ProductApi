namespace ProductApi.Middleware
{
    public class CustomException : IMiddleware
    {
        private readonly ILogger<CustomException> _logger;

        public CustomException(ILogger<CustomException> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
