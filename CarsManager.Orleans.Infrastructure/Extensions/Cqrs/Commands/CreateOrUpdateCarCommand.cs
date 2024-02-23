using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Commands;

public record CreateOrUpdateCarCommand(CarDetails Car) : IRequest
{
}

