using System;

namespace SQSWebApiConsumer.Models;

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