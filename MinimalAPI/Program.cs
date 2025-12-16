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

// ==========================================
// 1. CONFIGURACIÓN DE SERVICIOS (CONTENEDOR)
// ==========================================

// 💾 Conexión MySQL
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("MySQL");
    return new MySqlConnection(connectionString);
});

// 📦 Repositorios (CORREGIDO: Interfaz, Implementación)
// Esto es vital para que funcionen tus Controladores que piden IRepo...
builder.Services.AddScoped<IRepoCliente, RepoCliente>();
builder.Services.AddScoped<IRepoMarca, RepoMarca>();
builder.Services.AddScoped<IRepoProducto, RepoProducto>();
builder.Services.AddScoped<IRepoPedido, RepoPedido>();

// 🌐 Servicios MVC (Vistas y Controladores)
builder.Services.AddControllersWithViews();

// 🔐 Servicios de Sesión (Necesario por lo que vi en tu Index.cshtml)
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 📘 Scalar / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ==========================================
// 2. MIDDLEWARE (TUBERÍA DE PETICIONES)
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Para CSS, JS, imágenes
app.UseRouting();

app.UseSession(); // Activar Sesión

// 🚦 Rutas MVC (Por defecto va al Home o Login)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ==========================================
// 3. ENDPOINTS API (MINIMAL API)
// ==========================================
// Nota: Cambié "RepoCliente" por "IRepoCliente" para ser consistente

// CLIENTES
app.MapGet("/api/clientes", async (IRepoCliente repo) =>
{
    var clientes = await repo.ObtenerAsync();
    return Results.Ok(clientes);
});

app.MapGet("/api/clientes/{id}", async (int id, IRepoCliente repo) =>
{
    var cliente = await repo.DetalleAsync(id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
});

app.MapPost("/api/clientes", async (Cliente cliente, IRepoCliente repo) =>
{
    await repo.AltaAsync(cliente);
    return Results.Created($"/api/clientes/{cliente.IdCliente}", cliente);
});

// MARCAS
app.MapGet("/api/marcas", async (IRepoMarca repo) =>
{
    var marcas = await repo.ObtenerAsync();
    return Results.Ok(marcas);
});

app.MapGet("/api/marcas/{id}", async (int id, IRepoMarca repo) =>
{
    var marca = await repo.DetalleAsync(id);
    return marca is not null ? Results.Ok(marca) : Results.NotFound();
});

app.MapPost("/api/marcas", async (Topardex.Marca marca, IRepoMarca repo) =>
{
    await repo.AltaAsync(marca);
    return Results.Created($"/api/marcas/{marca.IdMarca}", marca);
});

// PRODUCTOS
app.MapGet("/api/productos", async (IRepoProducto repo) =>
{
    var productos = await repo.ObtenerAsync();
    return Results.Ok(productos);
});

app.MapGet("/api/productos/{id}", async (int id, IRepoProducto repo) =>
{
    var producto = await repo.DetalleAsync(id);
    return producto is not null ? Results.Ok(producto) : Results.NotFound();
});

app.MapPost("/api/productos", async (Topardex.Producto producto, IRepoProducto repo) =>
{
    await repo.AltaAsync(producto);
    return Results.Created($"/api/productos/{producto.IdProducto}", producto);
});

// PEDIDOS
app.MapGet("/api/pedidos", async (IRepoPedido repo) =>
{
    var pedidos = await repo.ObtenerAsync();
    return Results.Ok(pedidos);
});

app.MapPost("/api/pedidos", async (Pedido pedido, IRepoPedido repo) =>
{
    var nuevoPedido = await repo.AltaPedidoAsync(pedido);
    return Results.Created($"/api/pedidos/{nuevoPedido.IdPedido}", nuevoPedido);
});

app.Run();