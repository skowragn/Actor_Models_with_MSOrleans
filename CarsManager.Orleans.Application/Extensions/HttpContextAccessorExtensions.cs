using System.Security.Claims;

namespace CarsManager.Orleans.Application.Extensions;

internal static class HttpContextAccessorExtensions
{
    internal static string? TryGetUserId(
        this IHttpContextAccessor? httpContextAccessor) =>
        httpContextAccessor
            ?.HttpContext
            ?.User
            .FindFirstValue(ClaimTypes.NameIdentifier)
        ?? "demo-shared-user";
}
