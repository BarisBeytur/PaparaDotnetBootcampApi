using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Business.Services.Concrete;
using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.DataAccess.Repositories.UserRepository;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;

namespace PaparaDotnetBootcampApi.Extensions.ServiceCollectionExtension
{
    /// <summary>
    /// Program.cs içerisindeki DI (Dependency Injection) işlemleri bu extension içerisinde gerçekleştirilir.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Bu metot, DI (Dependency Injection) işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            // Generic repository and UnitOfWork dependency injection
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business services dependency injection
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
