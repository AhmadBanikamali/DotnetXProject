using Application.Common;
using MediatR;

namespace Application.Product.Command.AddProduct;

public record AddProductRequest(string Name, string Description, int Price) : IRequest<Response>;