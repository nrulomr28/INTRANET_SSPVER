using INTRANET_SSPVER.Models.Contexts;
using INTRANET_SSPVER.Models.Entities;
using INTRANET_SSPVER.Models.Services.Implementations;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// 🔹 Base de datos principal

var conString = builder.Configuration.GetConnectionString("conexion") ??
throw new InvalidOperationException("Connection string 'conexion' not found.");
builder.Services.AddDbContext<BdpagWebContext>(options =>
options.UseSqlServer(conString));


//builder.Services.AddDbContext<BdpagWebContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDirectorioService, DirectorioService>();

// Configurar licencia de QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

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

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Menu}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Inicio}/{controller=Menu}/{action=Index}/{id?}");


app.Run();
