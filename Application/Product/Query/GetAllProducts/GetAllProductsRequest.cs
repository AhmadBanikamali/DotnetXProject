using Application.Common;
using MediatR;

namespace Application.Product.Query.GetAllProducts;

public record GetAllProductsRequest : IRequest<Response>;