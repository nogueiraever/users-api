using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Users.Application.UseCases.Users.CreateUsers;

namespace Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.TryAddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.TryAddScoped<IUpdateUserEmailUseCase, UpdateUserEmailUseCase>();
            services.TryAddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            services.TryAddScoped<IRemoveUserByIdUseCase, RemoveUserByIdUseCase>();
            return services;
        }
    }
}
