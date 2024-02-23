using CarsManager.Orleans.Domain.Interfaces.Grains;
using CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Commands;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries.Handlers;

internal class CreateOrUpdateCarCommandHandler(IClusterClient client) : IRequestHandler<CreateOrUpdateCarCommand>
{
    private readonly IClusterClient _client = client;

    public Task Handle(CreateOrUpdateCarCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _client.GetGrain<ICarGrain>(request.Car.Id).CreateOrUpdateCarAsync(request.Car);

        return Task.CompletedTask;
    }
}