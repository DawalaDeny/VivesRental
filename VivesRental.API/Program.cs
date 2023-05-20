using Microsoft.EntityFrameworkCore;
using VivesRental.API.Controllers;
using VivesRental.Repository.Core;
using VivesRental.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderLineService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ArticleReservationService>();
builder.Services.AddScoped<ArticleService>();



builder.Services.AddDbContext<VivesRentalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VivesRentalDbContext"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<VivesRentalDbContext>();

    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
