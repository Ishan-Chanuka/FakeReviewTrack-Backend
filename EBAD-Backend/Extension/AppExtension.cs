using EBAD_Backend.Middleware;

namespace EBAD_Backend.Extension
{
    public static class AppExtension
    {
        public static void UseErrorHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandleMiddleware>();
        }
    }
}
