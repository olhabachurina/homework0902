using homework0902.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902
{
    public interface IUserService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int productId);
        Task<Order> PlaceOrder(int productId, int quantity, string customerId, string shippingAddress); 
    }
}

