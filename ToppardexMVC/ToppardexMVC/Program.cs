using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using System.Data;
using Topardex.top.Persistencia; // interfaces
using Topardex;                       // Entidades (Cliente, Pedido, etc.)        // Interfaces IRepo...
using Topardex.Ado.Dapper;
                 // Implementaciones Repo...// implementación Dapper

var builder = WebApplication.CreateBuilder(args);

// MVC con controladores y vistas
builder.Services.AddControllersWithViews();

// Configurar la conexión a MySQL (inyectable)
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("MySql")));

// Inyectar tus repositorios
builder.Services.AddScoped<IRepoCliente, RepoCliente>();
builder.Services.AddScoped<IRepoProducto, RepoProducto>();
builder.Services.AddScoped<IRepoMarca, RepoMarca>();
builder.Services.AddScoped<IRepoPedido, RepoPedido>();

var app = builder.Build();

// Configuración del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
