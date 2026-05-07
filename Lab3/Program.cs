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
    var dbContext = scope.ServiceProvider.GetRequiredService<DesignStudioDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.DesignServices.Any())
    {
        var webDesign = new DesignService { Name = "Дизайн сайту", Description = "Створення UI/UX сайту", BasePrice = 5000, IsCustom = false };
        var interiorDesign = new DesignService { Name = "Дизайн інтер'єру", Description = "Дизайн кімнати", BasePrice = 15000, IsCustom = false };

        var services = new List<DesignService>
        {
            webDesign,
            interiorDesign,
            new() { Name = "Дизайн Landing Page", Description = "Створення UI/UX односторінкового сайту", BasePrice = 6000, IsCustom = false },
            new() { Name = "Дизайн корпоративного сайту", Description = "Багатосторінковий сайт для компанії", BasePrice = 15000, IsCustom = false },
            new() { Name = "Інтернет-магазин \"під ключ\"", Description = "Комплексний дизайн e-commerce", BasePrice = 0, IsCustom = true },
            new() { Name = "Розробка логотипу", Description = "Створення унікального логотипу", BasePrice = 4000, IsCustom = false },
            new() { Name = "Базовий фірмовий стиль", Description = "Логотип, кольорова палітра, шрифти", BasePrice = 8000, IsCustom = false },
            new() { Name = "Комплексний брендинг", Description = "Айдентика компанії \"під ключ\"", BasePrice = 0, IsCustom = true },
            new() { Name = "UI/UX аудит", Description = "Аналіз інтерфейсу та звіт", BasePrice = 3000, IsCustom = false },
            new() { Name = "Дизайн мобільного додатку", Description = "Проєктування для iOS та Android", BasePrice = 0, IsCustom = true },
            new() { Name = "Оформлення соц. мереж", Description = "Шаблони для Instagram/Facebook", BasePrice = 3500, IsCustom = false },
            new() { Name = "Креативи для реклами", Description = "Пакет з 5 статичних банерів", BasePrice = 1500, IsCustom = false }
        };

        dbContext.DesignServices.AddRange(services);
        dbContext.SaveChanges();

        dbContext.PortfolioItems.AddRange(
            new PortfolioItem
            {
                Title = "Сайт для інтернет-магазину",
                ImageUrl = "https://fastly.picsum.photos/id/2/5000/3333.jpg?hmac=_KDkqQVttXw_nM-RyJfLImIbafFrqLsuGO5YuHqD-qQ",
                DesignServiceId = webDesign.Id
            },
            new PortfolioItem
            {
                Title = "Корпоративний портал",
                ImageUrl = "https://fastly.picsum.photos/id/60/1920/1200.jpg?hmac=fAMNjl4E_sG_WNUjdU39Kald5QAHQMh-_-TsIbbeDNI",
                DesignServiceId = webDesign.Id
            },
            new PortfolioItem
            {
                Title = "Мобільний застосунок",
                ImageUrl = "https://fastly.picsum.photos/id/160/3200/2119.jpg?hmac=cz68HnnDt3XttIwIFu5ymcvkCp-YbkEBAM-Zgq-4DHE",
                DesignServiceId = interiorDesign.Id
            }
        );

        dbContext.SaveChanges();
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