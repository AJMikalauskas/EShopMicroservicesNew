using BuildingBlocks.CQRS;
using Catalog.API.Models;
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description,
    string ImageFile, decimal Price)
 : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business logic to create a product
        // 1. Create Product Entity from Command Object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        // 2. Save to Database

        // 3. Return Result
        return new CreateProductResult(Guid.NewGuid());
    }
}