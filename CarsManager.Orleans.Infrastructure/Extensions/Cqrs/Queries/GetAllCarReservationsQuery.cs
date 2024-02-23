using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Infrustructure.Extensions.Cqrs.Queries;

public record GetAllCarReservationsQuery : IRequest<HashSet<CarDetails>>
{
   
}