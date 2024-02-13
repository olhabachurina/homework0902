using Fluent.Infrastructure.FluentModel;
using homework0902.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
namespace homework0902
{
    private readonly OnlineStoreContext _context;

    public UserService(OnlineStoreContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductById(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Order> PlaceOrder(int productId, int quantity, string customerId, string shippingAddress)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new ArgumentException("Товар не найден", nameof(productId));
        }

        if (product.Quantity < quantity)
        {
            throw new ArgumentException("Недостаточное количество товара", nameof(quantity));
        }

        // Создание заказа
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = product.Price * quantity,
            Status = "Pending",
            ShippingAddress = shippingAddress,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    PriceAtOrder = product.Price
                }
            }
        };

        return order;
    }
}