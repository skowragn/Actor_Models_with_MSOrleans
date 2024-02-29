using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Commands;

public record AddOrUpdateItemCommand(int Quantity, CarDetails Car) : IRequest<bool>
{
   
}