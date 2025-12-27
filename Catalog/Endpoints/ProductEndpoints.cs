using OpenTelemetry.Trace;

namespace Catalog.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProdutEndpoints(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/products");

            group.MapGet("/", async (ProductService service) =>
            {
                var products = await service.GetProductsAsync();
                return Results.Ok(products);
            }).WithName("GetAllProducts").Produces<List<Product>>(StatusCodes.Status200OK);

            group.MapGet("/{id}", async (int id,ProductService service) =>
            {
                var product = await service.GetProductByIdAsync(id);
                if (product is null) return Results.NotFound();

                return Results.Ok(product);
            }).WithName("GetProductById").Produces<List<Product>>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (Product inputProduct, ProductService service, TracerProvider tracerProvider) =>
            {
                var tracer = tracerProvider.GetTracer("ProductEndpoints");
                using var span = tracer.StartActiveSpan("CreateProduct");
                await service.CreateProductAsync(inputProduct);
                return Results.CreatedAtRoute("GetProductById", new { id = inputProduct.Id }, inputProduct);
               // return Results.Created($"/products/{inputProduct.Id}", inputProduct);
            }).WithName("CreateProduct").Produces<Product>(StatusCodes.Status201Created);

            group.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
            {
                var existingProduct = await service.GetProductByIdAsync(id);
                if (existingProduct is null) return Results.NotFound();
                await service.UpdateProductAsync(existingProduct, inputProduct);
                return Results.NoContent();
            }).WithName("UpdateProduct").Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id}", async (int id, ProductService service) =>
            {
                var existingProduct = await service.GetProductByIdAsync(id);
                if (existingProduct is null) return Results.NotFound();
                await service.DeleteProductAsync(existingProduct);
                return Results.NoContent();
            }).WithName("DeleteProduct").Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);
        }

    }
}
