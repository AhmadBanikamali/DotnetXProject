using Application.Common;
using MediatR;

namespace Application.Product.Command.UpdateProduct;

public record UpdateProductRequest(Guid Id,string Name, string Description, int Price):IRequest<Response>;