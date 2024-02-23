using CarsManager.Orleans.Infrastructure.Extensions;

namespace CarsManager.Orleans.Infrastructure.Services;

public class ClusterBaseServices 
{
    private readonly IHttpContextAccessor _httpContextAccessor = null!;
    private readonly IClusterClient _client = null!;

    public ClusterBaseServices(
        IHttpContextAccessor httpContextAccessor, IClusterClient client) =>
        (_httpContextAccessor, _client) = (httpContextAccessor, client);

    public T TryUseGrain<TGrainInterface, T>(
        Func<TGrainInterface, T> useGrain, Func<T> defaultValue)
        where TGrainInterface : IGrainWithStringKey =>
         TryUseGrain(
             useGrain,
             _httpContextAccessor.TryGetUserId(),
             defaultValue);

    public T TryUseGrain<TGrainInterface, T>(
        Func<TGrainInterface, T> useGrain,
        string? key,
        Func<T> defaultValue)
        where TGrainInterface : IGrainWithStringKey =>
        key is { Length: > 0 } primaryKey
            ? useGrain.Invoke(_client.GetGrain<TGrainInterface>(primaryKey))
            : defaultValue.Invoke();
}
