using Application.Common;
using MediatR;

namespace Application.Product.Query.GetProductById;

public record GetProductByIdRequest(Guid Id) : IRequest<Response>;