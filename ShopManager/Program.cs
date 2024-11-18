using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using ShopManager.Data;
using ShopManager.DomainModels;
using ShopManager.Features.Products;
using ShopManager.Features.Shops;
using ShopManager.Features.Shops.Validations;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(Ulid.NewUlid().ToGuid());
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
        opt.MapEnum<ActivationStatus>("activation_status");
        opt.MapEnum<PaymentStatus>("payment_status");
        opt.MapEnum<BillingCycle>("billing_cycle");
        opt.MapEnum<FairlyUsedItemCondition>("fairlyused_item_condition");
    });
    opt.EnableSensitiveDataLogging();
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
});

builder.Services.AddScoped<ShopQueryService>();
builder.Services.AddScoped<IValidator<CreateShopDto>, ValidateCreateShopDto>();
builder.Services.AddScoped<IValidator<UpdateShopDto>, ValidateUpdateShopDto>();
builder.Services.AddScoped<IShopCommandService, ShopsCommandService>();
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
app.MapShopEndpoints();
app.MapProductsEndpoints();

app.Run();
