using BLL.Interfaces;
using BLL.Mappings;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using DAL.UoW;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<DesignStudioDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Data Access Layer
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Business Logic Layer
builder.Services.AddScoped<IStudioService, StudioService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BLL.Mappings.MappingProfile>();
});

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<IStudioService>();
    if (!service.GetServices().Any())
    {
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Дизайн сайту", Description = "Створення UI/UX сайту", BasePrice = 5000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Дизайн інтер'єру", Description = "Дизайн кімнати до 50 кв.м.", BasePrice = 15000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Унікальний дизайн 'під ключ'", Description = "Комплексний індивідуальний дизайн", BasePrice = 0, IsCustom = true });
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route Razor Pages
app.MapRazorPages();

app.Run();