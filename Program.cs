using CarsMongoDbDemo.Models.Validators;
using CarsMongoDbDemo.Services;
using CarsMongoDbDemo.ViewModels.Car;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// MongoDb Service
builder.Services.Configure<CarStoreDatabaseSettings>(
    builder.Configuration.GetSection("CarStoreDatabase"));

builder.Services.AddSingleton<CarsService>();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation(); // Server Side
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // Client Sidebuilder.Services.AddScoped<IValidator<AddCarViewModel>, AddCarValidator>();
builder.Services.AddScoped<IValidator<UpdateCarViewModel>, UpdateCarValidator>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();