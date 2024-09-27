using CarsManager.Orleans.Domain;
using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Queries;

public record GetAllAvailableCarsDetailsQuery : IRequest<HashSet<CarDetails>>
{
   
}