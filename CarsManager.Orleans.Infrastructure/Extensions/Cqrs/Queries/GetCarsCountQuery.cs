using MediatR;

namespace CarsManager.Orleans.Infrastructure.Extensions.Cqrs.Queries;
public record GetCarsCountQuery : IRequest<int>
{
}
