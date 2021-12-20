using Application.Common.Dto;
using MediatR;
using Application.Interfaces.ProductServices;

namespace Application.Cqrs.Products.Command;

public class AddProductCommand : ProductDto, IRequest<Unit>
{

}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
{
    private readonly IProductCommandServices _productService;


    public AddProductCommandHandler(IProductCommandServices productService)
    {
        _productService = productService;
    }

    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.AddNewProduct(request, cancellationToken);
    }
}

