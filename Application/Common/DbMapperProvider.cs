using AutoMapper;
using Repository;

namespace Application;

public class DbMapperProvider
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public DbMapperProvider(IMapper mapper,IUnitOfWork unitOfWork)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
}

