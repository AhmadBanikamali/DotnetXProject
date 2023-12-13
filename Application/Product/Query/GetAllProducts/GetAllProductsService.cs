using Application.Common;
using AutoMapper;
using MediatR;
using Repository;

namespace Application.Product.Query.GetAllProducts;

public class GetAllProductsService : DbMapperProvider, IRequestHandler<GetAllProductsRequest, Response>
{
    public GetAllProductsService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<Response> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await UnitOfWork.ProductRepository.Get();
        return new Response(Data: Mapper.Map<ICollection<ProductItem>>(products));
    }
}