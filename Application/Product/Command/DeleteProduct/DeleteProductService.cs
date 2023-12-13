using Application.Common;
using AutoMapper;
using MediatR;
using Repository;

namespace Application.Product.Command.DeleteProduct;

public class DeleteProductService:DbMapperProvider,IRequestHandler<DeleteProductRequest,Response>
{
    public DeleteProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<Response> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await UnitOfWork.ProductRepository.Delete(Mapper.Map<Domain.Product>(request));
        await UnitOfWork.Save();
        return new Response();
    }
}