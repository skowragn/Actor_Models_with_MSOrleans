using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Queries;
public record GetCarsCountQuery : IRequest<int>
{
}
