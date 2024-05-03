using Cafe_App.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//Veri Taban� Ba�lant�s�
builder.Services.AddDbContext<IdentityDataContext>(Options =>
{
    var configuration = builder.Configuration.GetConnectionString("mysql_connection");
    var version = new MySqlServerVersion(new Version(8, 0, 36));
    Options.UseMySql(configuration, version);
});

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

app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Musteri",
    pattern: "{area:exists}/{controller=Musteri}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Admin}/{action=Index}/{id?}");
    //pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
