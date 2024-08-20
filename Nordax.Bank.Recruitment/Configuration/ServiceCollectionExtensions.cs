using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nordax.Bank.Recruitment.DataAccess;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Domain.Interfaces;
using Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
using Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Logic.Commands;
using Nordax.Bank.Recruitment.Logic.Queries;

namespace Nordax.Bank.Recruitment.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppConfig>(configuration.GetSection(nameof(AppConfig)));
        services.AddSingleton<IAppConfig>(s => s.GetRequiredService<IOptions<AppConfig>>().Value);
        services.AddTransient<ISubscriptionCommands, SubscriptionCommands>();
        services.AddTransient<ISubscriptionQueries, SubscriptionQueries>();
        services.AddTransient<ILoanCommands, LoanCommands>();
        services.AddTransient<ILoanQueries, LoanQueries>();
        services.AddEntityFramework(configuration);

        services.AddDataAccessServices();
    }

    public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddDataAccessServices(this IServiceCollection services)
    {
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
        services.AddTransient<ILoanRepository, LoanRepository>();
    }
}