namespace Api.Services;

public static class _ServiceLocator
{
    private static IServiceProvider? _services;

    public static void Init(IServiceProvider services)
    {
        _services = services;
    }

    public static T GetService<T>() => _services.GetService<T>();

}
