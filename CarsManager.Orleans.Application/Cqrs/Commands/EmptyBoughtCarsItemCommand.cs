using MediatR;

namespace CarsManager.Orleans.Application.Cqrs.Commands;

public record EmptyBoughtCarsItemCommand : IRequest
{
   
}