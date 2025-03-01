namespace Catalog.API.Products.CreateProduct;

// Ctrl + B --> opens and closes folders structure.
public record CreateProductRequest(string Name, List<string> Category, string Description,
    string ImageFile, decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // 1. Define HTTP Post endpoint uri
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) => {
                // 2. Map the request object to the command object
                var command = request.Adapt<CreateProductCommand>();

                // 3. Trigger the MediatR via sender from the MediatR class
                var result = await sender.Send(command);

                // 4. Map the result back to the response model
                var response = result.Adapt<CreateProductResponse>();

                // 5. Return the result with response and uri
                return Results.Created($"/products/{response.Id}", response);
            })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
        // These are all extension methods for helping to configure the HTTP POST endpoint even more.
    }

}