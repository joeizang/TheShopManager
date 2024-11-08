using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.DomainModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add EntityFramework support and Identity

builder.Services.AddDbContext<ShopManagerBaseContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Database=shopplatform;Username=postgres;Password=postgres", opt => {
        opt.UseNodaTime();
        opt.MapEnum<Currency>("currency");
        opt.MapEnum<PaymentMethod>("payment_method");
        opt.MapEnum<FairlyUsedItemCondition>("fairlyused_item_condition");
    });
    opt.EnableSensitiveDataLogging();
});

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ShopManagerBaseContext>()
    .AddApiEndpoints();

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapIdentityApi<ApplicationUser>();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
