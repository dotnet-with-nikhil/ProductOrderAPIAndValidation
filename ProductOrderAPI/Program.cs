using dotnet_example_clean_arch_with_entity_framework.Data;
using dotnet_example_clean_arch_with_entity_framework.Middlewares;
using dotnet_example_clean_arch_with_entity_framework.Repositories;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Services;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//configuration logger

Log.Logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .Enrich.WithThreadId()
             .Enrich.WithMachineName()
             .WriteTo.Console()
             .WriteTo.File(
                path: "Logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate:
                "{Timestamp:yyyy-MM-dd HH:mm:ss} [Level] {Message} {Properties} {NewLine}{Exception}"
            )
             .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();

//Register the DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Register the Respistories

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

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
