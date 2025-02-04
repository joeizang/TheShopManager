using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using NodaTime.Text;
using ShopManager.Core;
using ShopManager.Core.Middleware;
using ShopManager.Data;
using ShopManager.DomainModels;
using ShopManager.Features.Shops;
using ShopManager.Features.Shops.Abstractions;
using ShopManager.Features.Shops.Categories;
using ShopManager.Features.Shops.Categories.Validations;
using ShopManager.Features.Shops.DomainModels;
using ShopManager.Features.Shops.Products;
using ShopManager.Features.Shops.Services;
using ShopManager.Features.Shops.Validations;
using ShopManager.Features.Tenants;
using ShopManager.Features.Tenants.Abstractions;
using ShopManager.Features.Tenants.Dtos;
using ShopManager.Features.Tenants.Services;
using ShopManager.Features.Tenants.Validations;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(Ulid.NewUlid().ToGuid());
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add EntityFramework support and Identity

// builder.Services.AddScoped<IShopManagerContextAccessor, ShopManagerContextAccessor>();

builder.Services.AddDbContext<ShopManagerBaseContext>(opt =>
{
    opt.UseNpgsql("Host=localhost;Database=shopplatform;Username=postgres;Password=postgres;Port=5432", opt => {
        opt.UseNodaTime();
        opt.MapEnum<Currency>("currency");
        opt.MapEnum<PaymentMethod>("payment_method");
        opt.MapEnum<ActivationStatus>("activation_status");
        opt.MapEnum<PaymentStatus>("payment_status");
        opt.MapEnum<BillingCycle>("billing_cycle");
        opt.MapEnum<FairlyUsedItemCondition>("fairlyused_item_condition");
        opt.MapEnum<InvoiceStatus>("invoice_status");
    });
    opt.EnableSensitiveDataLogging();
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    options.SerializerOptions.Converters.Add(
        new NodaPatternConverter<ZonedDateTime>(ZonedDateTimePattern.ExtendedFormatOnlyIso));
});

builder.Services.AddScoped<ShopQueryService>();
builder.Services.AddScoped<IValidator<CreateShopDto>, ValidateCreateShopDto>();
builder.Services.AddScoped<IValidator<UpdateShopDto>, ValidateUpdateShopDto>();
builder.Services.AddScoped<IValidator<CreateSubscriptionPlanTypeDto>, ValidateCreateSubscriptionPlanType>();
builder.Services.AddScoped<IValidator<CreateSubscriptionPlanDto>, ValidateCreateSubscriptionPlan>();
builder.Services.AddScoped<IValidator<CreateTenantDto>, ValidateCreateTenant>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, ValidateCreateCategoryDto>();

builder.Services.AddScoped<IShopCommandService, ShopsCommandService>();
builder.Services.AddScoped<ITenantCommandService, TenantCommandService>();
builder.Services.AddScoped<ISubscriptionPlan, SubscriptionPlanService>();
builder.Services.AddScoped<ITenantPaymentMethodCommandService, TenantPaymentMethodCommandService>();
builder.Services.AddScoped<ITenantPaymentCommandService, TenantPaymentCommandService>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
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
app.UseMiddleware<ShopIdentityResolverMiddleware>();
app.MapShopEndpoints();
app.MapTenantEndpoints();
app.MapSubscriptionPlanEndpoints();
app.MapSubscriptionPlanTypeEndpoints();

app.Run();