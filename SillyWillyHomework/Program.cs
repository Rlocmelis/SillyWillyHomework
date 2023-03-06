using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SillyWillyHomework.Business.Discounts;
using SillyWillyHomework.DbContexts;
using SillyWillyHomework.Entities;
using SillyWillyHomework.ErrorHandling;
using SillyWillyHomework.Models;
using SillyWillyHomework.Models.Requests;
using SillyWillyHomework.Repositories.BaseRepository;
using SillyWillyHomework.Services;
using SillyWillyHomework.Services.BaseService;
using SillyWillyHomework.Validation.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("db"));

// Call SeedData method after registering ApplicationDbContext
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    ApplicationDbContext.SeedData(context);
}

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register base Repository and any repositories that imlement it
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

// Register the services that implement the BaseService
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddScoped<IValidator<OrderRequest>, OrderRequestDtoValidator>();
builder.Services.AddScoped<IValidator<OrderItemRequest>, OrderRequestItemDtoValidator>();
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();

builder.Services.AddScoped<IDiscountCalculator, DefaultDiscountCalculator>();
builder.Services.AddScoped<IProductDiscountService, ProductDiscountService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
