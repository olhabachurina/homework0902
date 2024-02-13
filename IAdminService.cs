using homework0902.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902
{
    public interface IAdminService
    {
        Task AddProduct(Product product);
        Task UpdateProduct(int productId, Product product);
        Task DeleteProduct(int productId);
        Task<Product> GetProductById(int id);
        Task SaveChangesAsync();
    }
}
