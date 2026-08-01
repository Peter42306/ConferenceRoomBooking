
using ConferenceRoomBooking.Application.Interfaces.Services;
using ConferenceRoomBooking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceRoomBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();

            return services;
        }
    }
}
