using Application.Common;
using MediatR;

namespace Application.Product.Command.DeleteProduct;

public record DeleteProductRequest(Guid Id):IRequest<Response>;