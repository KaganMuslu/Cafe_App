using Cafe_App.Areas.Admin.Validators;
using Cafe_App.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IdentityDataContext>();

// Veri Tabaný Baðlantýsý
builder.Services.AddDbContext<IdentityDataContext>(Options =>
{
    var configuration = builder.Configuration.GetConnectionString("mysql_connection");
    var version = new MySqlServerVersion(new Version(8, 0, 36));
    Options.UseMySql(configuration, version);
});

// Oturum (Session) Hizmetlerini Ekle
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".CafeApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Oturumlarý Kullan
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Musteri",
    pattern: "{area:exists}/{controller=Musteri}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Garson",
    pattern: "{area:exists}/{controller=Garson}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "Mutfak",
	pattern: "{area:exists}/{controller=Mutfak}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Mutfak}/{controller=Mutfak}/{action=Index}/{id?}");

app.Run();
