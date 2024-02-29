using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Commands;

public record CreateOrUpdateCarCommand(CarDetails Car) : IRequest
{
}

