using CarsManager.Orleans.Domain.Interfaces.Grains;
using CarsManager.Orleans.Application.Cqrs.Commands;
using MediatR;

namespace CarsManager.Orleans.Application.Extensions.Cqrs.Queries.Handlers;

internal class CreateOrUpdateAvailableCarDetailsCommandHandler(IClusterClient client) : IRequestHandler<CreateOrUpdateAvailableCarDetailsCommand>
{
    private readonly IClusterClient _client = client;

    public Task Handle(CreateOrUpdateAvailableCarDetailsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _client.GetGrain<ICarGrain>(request.Car.Id).CreateOrUpdateCarAsync(request.Car);

        return Task.CompletedTask;
    }
}