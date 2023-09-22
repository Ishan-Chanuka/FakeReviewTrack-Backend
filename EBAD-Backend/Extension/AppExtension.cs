using EBAD_Backend.DataAccess;
using EBAD_Backend.DataAccess.Concrete;
using EBAD_Backend.DataAccess.Interface;
using EBAD_Backend.Middleware;
using EBAD_Backend.Services.Concrete;
using EBAD_Backend.Services.Interface;

namespace EBAD_Backend.Extension
{
    public static class AppExtension
    {
        public static void UseErrorHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandleMiddleware>();
        }

        public static void DatabaseExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("MongoConnection"));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDataAccess, DataAccessService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
