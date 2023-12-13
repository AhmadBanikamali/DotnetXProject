using Application.Common;
using AutoMapper;
using MediatR;
using Repository;

namespace Application.Product.Command.UpdateProduct;

public class UpdateProductService : DbMapperProvider, IRequestHandler<UpdateProductRequest, Response>
{
    public UpdateProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<Response> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        await UnitOfWork.ProductRepository.Update(Mapper.Map<Domain.Product>(request));
        await UnitOfWork.Save();
        return new Response();
    }
}