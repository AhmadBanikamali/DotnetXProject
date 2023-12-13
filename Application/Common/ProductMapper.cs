using Application.Product.Command.AddProduct;
using Application.Product.Command.DeleteProduct;
using Application.Product.Command.UpdateProduct;
using Application.Product.Query.GetAllProducts;
using Application.Product.Query.GetProductById;
using AutoMapper;

namespace Application.Common;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Domain.Product, ProductItem>();
        CreateMap<Domain.Product, ProductDetail>();
        CreateMap<AddProductRequest, Domain.Product>();
        CreateMap<UpdateProductRequest, Domain.Product>();
        CreateMap<DeleteProductRequest, Domain.Product>();
    }
}