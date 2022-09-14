using StackExchange.Profiling.Storage;

namespace Architecture.Web;

public static class Extintion
{
    public static void AddProfiler(this IServiceCollection service)
    {
        service.AddMiniProfiler(option =>
        {
            option.RouteBasePath = "/api/profiler";
            option.ShowControls = true;
            option.PopupShowTrivial = true;
            option.StackMaxLength = 1000;
            (option.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
            option.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();

        }).AddEntityFramework();
    }
}
