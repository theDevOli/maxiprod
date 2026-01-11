using Maxiprod.Application.Services.BalanceService;
using Maxiprod.Application.Services.CategoryService;
using Maxiprod.Application.Services.PersonService;
using Maxiprod.Application.Services.TransactionService;
using Maxiprod.Application.ServicesContracts.BalanceContracts;
using Maxiprod.Application.ServicesContracts.CategoryContracts;
using Maxiprod.Application.ServicesContracts.PersonContracts;
using Maxiprod.Application.ServicesContracts.TransactionContracts;
using Maxiprod.Domain.RepositoryContract;
using Maxiprod.Infrastructure.DbContext;
using Maxiprod.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Maxiprod.Infrastructure.Extensions;

/// <summary>
/// Extension methods for IServiceCollection to add infrastructure services and repositories.
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds repository implementations to the service collection.
    /// </summary>
    /// <param name="services">
    /// The service collection to add repositories to.
    /// </param>
    /// <returns>The updated service collection.</returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBalanceRepository, BalanceRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }

    /// <summary>
    /// Adds service implementations to the service collection.
    /// </summary>
    /// <param name="services">
    /// The service collection to add services to.
    /// </param>
    /// <returns>
    /// The updated service collection.
    /// </returns>
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBalanceService, BalanceService>();

        services.AddScoped<ICategoryAdderService, CategoryAdderService>();
        services.AddScoped<ICategoryDeletionService, CategoryDeletionService>();
        services.AddScoped<ICategoryGetterByIdService, CategoryGetterByIdService>();
        services.AddScoped<ICategoryGetterService, CategoryGetterService>();
        services.AddScoped<ICategoryUpdatableService, CategoryUpdatableService>();

        services.AddScoped<IPersonAdderService, PersonAdderService>();
        services.AddScoped<IPersonDeletionService, PersonDeletionService>();
        services.AddScoped<IPersonGetterByIdService, PersonGetterByIdService>();
        services.AddScoped<IPersonGetterService, PersonGetterService>();
        services.AddScoped<IPersonUpdatableService, PersonUpdatableService>();

        services.AddScoped<ITransactionAdderService, TransactionAdderService>();
        services.AddScoped<ITransactionDeletionService, TransactionDeletionService>();
        services.AddScoped<ITransactionGetterByIdService, TransactionGetterByIdService>();
        services.AddScoped<ITransactionGetterService, TransactionGetterService>();
        services.AddScoped<ITransactionUpdatableService, TransactionUpdatableService>();

        return services;
    }

    /// <summary>
    /// Adds all infrastructure services and repositories to the service collection.
    /// </summary>
    /// <param name="services">
    /// The service collection to add infrastructure services and repositories to.
    /// </param>
    /// <returns>
    /// The updated service collection.
    /// </returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRepositories();

        services.AddServices();

        services.AddScoped<DataContext>();

        return services;
    }

}
