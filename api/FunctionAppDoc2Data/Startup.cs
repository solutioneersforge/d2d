using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Middleware;
using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

[assembly: FunctionsStartup(typeof(FunctionAppDoc2Data.Startup))]
namespace FunctionAppDoc2Data;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(Environment.CurrentDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

        var dbConnString = Environment.GetEnvironmentVariable("SqlConnectionString");

        builder.Services.AddSingleton<IConfiguration>(config);
        builder.Services.AddScoped<IExpenseSubExpenseRepository, ExpenseSubExpenseRepository>();
        builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
        builder.Services.AddScoped<IReceiptRespository, ReceiptRespository>();
        builder.Services.AddScoped<IReceiptDashbboardRepository, ReceiptDashbboardRepository>();
        builder.Services.AddScoped<IReceiptVerificationRepository, ReceiptVerificationRepository>();
        builder.Services.AddScoped<IReceiptApprovalRepository, ReceiptApprovalRepository>();
        builder.Services.AddScoped<IReceiptHistoryRepository, ReceiptHistoryRepository>();
        builder.Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
        builder.Services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
        builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
        builder.Services.AddScoped<IAppRegistrationRepository, AppRegistrationRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IValidatedTokenService, ValidatedTokenService>();
        builder.Services.AddScoped<IRolesRepository, RolesRepository>();
        builder.Services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
        builder.Services.AddDbContext<DocToDataDBContext>(options =>
                options.UseSqlServer(dbConnString), ServiceLifetime.Scoped);

    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        var host = new HostBuilder()
     .ConfigureFunctionsWorkerDefaults(builder =>
     {
         builder.UseMiddleware<JwtMiddleware>();
     })
     .Build();
    }
}
