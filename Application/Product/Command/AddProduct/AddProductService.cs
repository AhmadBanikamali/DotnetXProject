using Application.Common;
using AutoMapper;
using MediatR;
using Repository;
using Repository.DbContexts;

namespace Application.Product.Command.AddProduct;

public class AddProductService : DbMapperProvider, IRequestHandler<AddProductRequest, Response>
{
    private readonly ApplicationDbContext _context;

    public AddProductService(ApplicationDbContext context,IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _context = context;
    }

    public async Task<Response> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        var product = Mapper.Map<Domain.Product>(request);
        await UnitOfWork.ProductRepository.Insert(product);
        await UnitOfWork.Save();
        return new Response();
    }
}