
using Vives.SDK;
using VivesRental.UI.SDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("VivesRentalAPI",
    options => { options.BaseAddress = new Uri("https://localhost:7034/"); });

builder.Services.AddScoped<CustomerManagementSdk>();
builder.Services.AddScoped<ProductManagementSdk>();
builder.Services.AddScoped<ArticleSdk>();
builder.Services.AddScoped<OrderSdk>();
builder.Services.AddScoped<ReservationSdk>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
