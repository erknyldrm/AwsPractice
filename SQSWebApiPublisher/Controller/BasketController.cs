using Microsoft.AspNetCore.Mvc;
using SQSWebApiPublisher.Messaging;
using SQSWebApiPublisher.Models;

namespace SQSWebApiPublisher.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController(SendMessage sqs) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            List<Basket> baskets = Basket.GetAll();
            List<Order> orders = [];

            foreach (var basket in baskets)
            {
                Order order = new() {
                    Price = basket.Price,
                    ProductName = basket.ProductName,
                    Quantity = basket.Quantity
                };

                orders.Add(order);
            }

            await sqs.SendMessageAsync(orders);

            return Ok(new { Message = "Sipariş oluşturuldu."});
        }
    }
}
