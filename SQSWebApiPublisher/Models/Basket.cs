using System;

namespace SQSWebApiPublisher.Models;

public sealed class Basket
{
    public Basket()
    {
        Id = Guid.NewGuid();
    }


    public Guid Id { get; set; }
    public string ProductName { get; set; } = String.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public static List<Basket> GetAll()
    {
        return new List<Basket> {
            new Basket() {
             ProductName = "MacBook",
             Quantity = 1,
             Price = 1500
            },
            new Basket{
                ProductName = "Iphone",
                Quantity = 1,
                Price = 1200
            }
        };
    }
}

public sealed class Order
{
    public Order()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
    public string ProductName { get; set; } = String.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
