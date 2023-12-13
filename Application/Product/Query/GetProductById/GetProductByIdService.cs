using Application.Common;
using AutoMapper;
using MediatR;
using Repository;

namespace Application.Product.Query.GetProductById;

public class GetProductByIdService : DbMapperProvider, IRequestHandler<GetProductByIdRequest, Response>
{
    public GetProductByIdService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<Response> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await UnitOfWork.ProductRepository.GetById(request.Id);
        return product != null
            ? new Response(Data: Mapper.Map<ProductDetail>(product))
            : new Response(IsSuccess: false, Message: "product not found");
    }
}