using OrderManagerAPI.Order;

namespace OrderManagerAPI.Data;

public static class DataSeeder
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
    }

    private static void SeedData(AppDbContext context)
    {
        if (context.Orders.Any())
        {
            Console.WriteLine("--> We already have data, no need to seed data at startup");
            return;
        }

        Console.WriteLine("--> Seeding Data...");

        context.Orders.AddRange(
            new Order.Order
            {
                UserId = 1,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 231,
                        Amount = 12,
                    },
                    new Book
                    {
                        ProductId = 2312,
                        Amount = 122,
                    },
                    new Book
                    {
                        ProductId = 231342,
                        Amount = 12,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            },
            new Order.Order
            {
                UserId = 1,
                CancelledAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 231,
                        Amount = 12,
                    },
                    new Book
                    {
                        ProductId = 2312,
                        Amount = 122,
                    },
                    new Book
                    {
                        ProductId = 231342,
                        Amount = 12,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                CancelledAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            },
            new Order.Order
            {
                UserId = 1,
                ConfirmedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 231,
                        Amount = 12,
                    },
                    new Book
                    {
                        ProductId = 2312,
                        Amount = 122,
                    },
                    new Book
                    {
                        ProductId = 231342,
                        Amount = 12,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                ConfirmedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            },
            new Order.Order
            {
                UserId = 1,
                ConfirmedAt = DateTime.UtcNow.AddDays(-1),
                ShippedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 231,
                        Amount = 12,
                    },
                    new Book
                    {
                        ProductId = 2312,
                        Amount = 122,
                    },
                    new Book
                    {
                        ProductId = 231342,
                        Amount = 12,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                ConfirmedAt = DateTime.UtcNow.AddDays(-1),
                ShippedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                ConfirmedAt = DateTime.UtcNow.AddDays(-2),
                ShippedAt = DateTime.UtcNow.AddDays(-1),
                CompletedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            },
            new Order.Order
            {
                UserId = 13,
                ConfirmedAt = DateTime.UtcNow.AddDays(-2),
                ShippedAt = DateTime.UtcNow.AddDays(-1),
                CompletedAt = DateTime.UtcNow,
                Books = new List<Book>()
                {
                    new Book
                    {
                        ProductId = 2231,
                        Amount = 123,
                    },
                    new Book
                    {
                        ProductId = 1322,
                        Amount = 1234,
                    },
                    new Book
                    {
                        ProductId = 243,
                        Amount = 12321,
                    },
                }
            });

        context.SaveChanges();
    }
}
