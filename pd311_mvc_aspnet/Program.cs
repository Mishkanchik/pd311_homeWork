using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data.Initializer;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Repositories.Products;
using pd311_mvc_aspnet.Services.Image;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IImageService, ImageService>();

// Add repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("name=SQLServerLocal");
});

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

// Seeding data
app.Seed();

app.Run();
