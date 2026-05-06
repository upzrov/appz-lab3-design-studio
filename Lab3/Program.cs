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
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Дизайн Landing Page", Description = "Створення UI/UX односторінкового сайту для продажів", BasePrice = 6000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Дизайн корпоративного сайту", Description = "Багатосторінковий сайт для компанії (до 10 сторінок)", BasePrice = 15000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Інтернет-магазин \"під ключ\"", Description = "Комплексний дизайн e-commerce проєкту з унікальним функціоналом", BasePrice = 0, IsCustom = true });

        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Розробка логотипу", Description = "Створення унікального логотипу (3 концепти)", BasePrice = 4000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Базовий фірмовий стиль", Description = "Логотип, кольорова палітра, шрифти та візитки", BasePrice = 8000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Комплексний брендинг / Брендбук", Description = "Глобальна розробка айдентики компанії \"під ключ\"", BasePrice = 0, IsCustom = true });

        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "UI/UX аудит існуючого продукту", Description = "Аналіз інтерфейсу та звіт з рекомендаціями", BasePrice = 3000, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Дизайн мобільного додатку \"під ключ\"", Description = "Проєктування додатку для iOS та Android з нуля", BasePrice = 0, IsCustom = true });

        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Оформлення соціальних мереж", Description = "Шаблони для Instagram/Facebook (пости, сторіз, аватар)", BasePrice = 3500, IsCustom = false });
        service.AddService(new BLL.DTOs.DesignServiceDTO { Name = "Креативи для таргетованої реклами", Description = "Пакет з 5 статичних банерів", BasePrice = 1500, IsCustom = false });
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