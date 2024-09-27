using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Commands;

public record AddOrUpdateBoughtCarsItemCommand(int Quantity, CarDetails Car) : IRequest<bool>
{
   
}