using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;

public record AddOrUpdateItemCommand(int Quantity, CarDetails Car) : IRequest<bool>
{
   
}