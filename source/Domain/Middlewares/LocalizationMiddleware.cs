using System.Globalization;
using Architecture.Domain.Extension;
using Microsoft.AspNetCore.Http;

namespace Architecture.Domain.Middlewares;

public class LocalizationMiddleware
{
    private readonly RequestDelegate next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userLang = context.Request.Headers["Accept-Language"].ToString();
        var isArabic = !userLang.IsNullOrEmpty().Or(userLang.Contains("ar")).Or(userLang.Contains("en")).Not();

        var culture = new CultureInfo(isArabic ? "ar-ae" : "en");

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        await next(context);
    }
}
