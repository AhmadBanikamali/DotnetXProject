using Application.Product.Command.AddProduct;
using Application.Product.Command.DeleteProduct;
using Application.Product.Command.UpdateProduct;
using Application.Product.Query.GetAllProducts;
using Application.Product.Query.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllProductsRequest());
            return new JsonResult(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return new JsonResult(await _mediator.Send(new GetProductByIdRequest(id)));
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductRequest value)
        {
            return new JsonResult(await _mediator.Send(value));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductRequest value)
        {
            return new JsonResult(await _mediator.Send(value));
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductRequest value)
        {
            return new JsonResult(await _mediator.Send(value));
        }
    }
}
