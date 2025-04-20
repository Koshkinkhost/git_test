using api_for_kursach.Services;
using api_for_kursach.ViewModels;

namespace api_for_kursach
{
    public interface IRegistrationResponseFactory
    {
        RegistrationResponse CreateFailureResponse(Dictionary<string, string[]> messages);
        RegistrationResponse CreateSuccessResponse(Dictionary<string, string[]> messages,int id);
    }
    public class FactoryRegistrationResponse:IRegistrationResponseFactory
    {

        public RegistrationResponse CreateFailureResponse(Dictionary<string, string[]> messages)
        {
            return new RegistrationResponse
            {
                
                Success = false,
                messages = messages
            };
        }
        public RegistrationResponse CreateSuccessResponse(Dictionary<string, string[]> messages, int id)
        {
            return new RegistrationResponse
            {
                Id = id,
                Success = true,
                messages = messages
            };
        }
        

    }
    public static class ServiceProviderUserExtensions
    {
        public static void AddFactoryService(this IServiceCollection services)
        {
            services.AddScoped<IRegistrationResponseFactory,FactoryRegistrationResponse>();
        }
    }
}
