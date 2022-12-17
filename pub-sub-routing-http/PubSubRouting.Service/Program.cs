using PubSubRouting.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddDaprClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCloudEvents();

app.MapPost("/inventory/widgets", (Widget widget) =>
{
    Console.WriteLine("Subscriber received : " + widget);
    return Results.Ok();
});

app.MapPost("/inventory/gadgets", (Gadget gadget) =>
{
    Console.WriteLine("Subscriber received : " + gadget);
    return Results.Ok();
});

app.MapPost("/inventory/products", (Product product) =>
{
    Console.WriteLine("Subscriber received (default) : " + product);
    return Results.Ok();
});

app.MapPost("/failedMessages", (object message) =>
{
    Console.WriteLine("Subscriber received (dead letter) : " + message);
    return Results.Ok();
});

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
