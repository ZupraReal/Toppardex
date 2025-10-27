using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using System.Data;
using Topardex.top.Persistencia; 
using Topardex;                       
using Topardex.Ado.Dapper;

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
// Después de builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); // <--- esto registra IHttpContextAccessor


// ------------------ SESIÓN ------------------
// Permite usar HttpContext.Session en los controladores
builder.Services.AddDistributedMemoryCache(); // almacenamiento en memoria para sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tiempo de expiración
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// --------------------------------------------

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

app.UseSession(); // <- Middleware de sesión
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
