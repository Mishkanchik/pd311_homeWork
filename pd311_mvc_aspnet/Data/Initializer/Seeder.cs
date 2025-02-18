using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Data.Initializer
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>()
                                   ?? throw new ArgumentNullException("Db context is null");

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                    {
                        new() { Id = Guid.NewGuid().ToString(), Name = "Процесори" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Материнські плати" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Блоки живлення" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Оперативна пам'ять" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Відеокарти" }
                    };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                var products = new List<Product>
                    {
                        new() { Id = Guid.NewGuid().ToString(), Name = "Asus PRIME B650M-K (sAM5, AMD B650, PCI-Ex16)", Amount = 5, Price = 100, CategoryId = categories[1].Id },
                        new() { Id = Guid.NewGuid().ToString(), Name = "AMD Ryzen 5 5600 3.5-4.4GHz", Amount = 5, Price = 100, CategoryId = categories[0].Id }
                    };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
