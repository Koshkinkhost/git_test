using api_for_kursach.Repositories;
using api_for_kursach.Services;

namespace api_for_kursach
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAristRepository, ArtistRepository>();

            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IAlbumService, AlbumService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IStudioService, StudiosService>();
            services.AddScoped<IStudioRepository, StudioRepository>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRoyaltiRepository, RoyaltiRepository>();
            services.AddScoped<IRoyaltyService, RoyaltyService>();

            services.AddScoped<IRadioService, RadioService>();
            services.AddScoped<IRadioRepository, RadioRepository>();

            services.AddScoped<IRegistrationResponseFactory, FactoryRegistrationResponse>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();


            return services;
        }
    }
}
