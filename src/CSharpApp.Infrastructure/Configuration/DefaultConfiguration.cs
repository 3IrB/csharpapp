namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<ITodoService, TodoService>();

        services.AddSingleton<ITodoPostService, TodoPostService>();

        return services;
    }
}