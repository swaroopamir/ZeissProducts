using Microsoft.EntityFrameworkCore;
using ZeissProducts.Business.Services.Products;
using ZeissProducts.Data;
using ZeissProducts.Data.Repositories;
using ZeissProducts.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Register the DbContext
builder.Services.AddDbContext<ZeissDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZeissDbConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Dependency Injection
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ZeissExceptionHandler>();


builder.Services.AddExceptionHandler<ZeissExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
