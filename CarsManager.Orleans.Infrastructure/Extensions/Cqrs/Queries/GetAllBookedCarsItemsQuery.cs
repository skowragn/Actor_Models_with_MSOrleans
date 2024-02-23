using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;

public record GetAllBookedCarsItemsQuery : IRequest<HashSet<CarsBookedItem>>
{
   
}