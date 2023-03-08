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
builder.Services.AddScoped(typeof(IBaseRepository<Customer>), typeof(BaseRepository<Customer>));

// Register the services that implement the BaseService
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddScoped<IValidator<OrderRequest>, OrderRequestValidator>();
builder.Services.AddScoped<IValidator<OrderItemRequest>, OrderItemRequestValidator>();
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerRequestValidator>();

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

// Added so integration tests can access the Program.cs
public partial class Program { }