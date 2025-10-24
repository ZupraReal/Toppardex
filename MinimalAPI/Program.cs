using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using Topardex.Ado.Dapper;
using Topardex.top.Persistencia;
using Scalar.AspNetCore;
using Dapper;
using Topardex;

var builder = WebApplication.CreateBuilder(args);

// 💾 Conexión MySQL
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("MySQL");
    return new MySqlConnection(connectionString);
});

// 📦 Repositorios
builder.Services.AddScoped<RepoCliente>();
builder.Services.AddScoped<RepoMarca>();
builder.Services.AddScoped<RepoProducto>();
builder.Services.AddScoped<RepoPedido>();


// 📘 Scalar (documentación visual minimalista)
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// 🔹 Habilitar Scalar
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();


// ========================
//     ENDPOINTS
// ========================

// CLIENTES
app.MapGet("/clientes", async (RepoCliente repo) =>
{
    var clientes = await repo.ObtenerAsync();
    return Results.Ok(clientes);
});

app.MapGet("/clientes/{id}", async (int id, RepoCliente repo) =>
{
    var cliente = await repo.DetalleAsync(id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
});

app.MapPost("/clientes", async (Cliente cliente, RepoCliente repo) =>
{
    await repo.AltaAsync(cliente);
    return Results.Created($"/clientes/{cliente.IdCliente}", cliente);
});

// MARCAS
app.MapGet("/marcas", async (RepoMarca repo) =>
{
    var marcas = await repo.ObtenerAsync();
    return Results.Ok(marcas);
});

app.MapGet("/marcas/{id}", async (int id, RepoMarca repo) =>
{
    var marca = await repo.DetalleAsync(id);
    return marca is not null ? Results.Ok(marca) : Results.NotFound();
});

app.MapPost("/marcas", async (Topardex.Marca marca, RepoMarca repo) =>
{
    await repo.AltaAsync(marca);
    return Results.Created($"/marcas/{marca.IdMarca}", marca);
});

// PRODUCTOS
app.MapGet("/productos", async (RepoProducto repo) =>
{
    var productos = await repo.ObtenerAsync();
    return Results.Ok(productos);
});

app.MapGet("/productos/{id}", async (int id, RepoProducto repo) =>
{
    var producto = await repo.DetalleAsync(id);
    return producto is not null ? Results.Ok(producto) : Results.NotFound();
});

app.MapPost("/productos", async (Topardex.Producto producto, RepoProducto repo) =>
{
    await repo.AltaAsync(producto);
    return Results.Created($"/productos/{producto.IdProducto}", producto);
});

// PEDIDOS
app.MapGet("/pedidos", async (RepoPedido repo) =>
{
    var pedidos = await repo.ObtenerAsync();
    return Results.Ok(pedidos);
});

app.MapGet("/pedidos/{id}", async (int id, RepoPedido repo) =>
{
    var pedido = await repo.DetalleAsync(id);
    return pedido is not null ? Results.Ok(pedido) : Results.NotFound();
});

app.MapPost("/pedidos", async (Pedido pedido, RepoPedido repo) =>
{
    var nuevoPedido = await repo.AltaPedidoAsync(pedido);
    return Results.Created($"/pedidos/{nuevoPedido.IdPedido}", nuevoPedido);
});


app.Run();
