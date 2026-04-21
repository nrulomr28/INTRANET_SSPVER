using INTRANET_SSPVER.Models.Authentication;
using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Roles;
using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// 🔹 Base de datos principal

var conString = builder.Configuration.GetConnectionString("conexion") ??
throw new InvalidOperationException("Connection string 'conexion' not found.");
builder.Services.AddDbContext<BdpagWebContext>(options =>
options.UseSqlServer(conString));


// 🔹 Base de datos para autenticación
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));



// 🔹 Identity y configuración de contraseñas
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 4;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
//.AddEntityFrameworkStores<AuthDbContext>()
//.AddDefaultTokenProviders();




// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDirectorioService, DirectorioService>();
builder.Services.AddScoped<ICaleaService, CaleaService>();
builder.Services.AddScoped<IUbFisicaService, UbicacionFisicaService>();
builder.Services.AddScoped<ITransparenciaService, TransparenciaService>();
builder.Services.AddScoped<IAvisoPrivacidadService, AvisoPrivacidadService>();
builder.Services.AddScoped<IRolCatalogoService, RolCatalogoService>();
builder.Services.AddScoped<RolesService>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();






// Configurar licencia de QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();


// 🔹 Inicialización de roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CrearRoles.CrearRolesAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear roles: {ex.Message}");
    }
}





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Menu}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Inicio}/{controller=Menu}/{action=Index}/{id?}");

// 🔹 Ruta para áreas (Admin, Account, etc.)

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Account}/{controller=Account}/{action=Login}/{id?}");




app.Run();
