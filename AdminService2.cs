using homework0902.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902
{
    public class AdminService2 : IAdminService
    {
        private readonly OnlineStoreContext _context;

        public AdminService2(OnlineStoreContext context)
        {
            _context = context;
        }

      
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateProduct(int productId, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                throw new ArgumentException("Товар не найден", nameof(productId));
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            await _context.SaveChangesAsync();
        }

        
        public async Task DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException("Товар не найден", nameof(productId));
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

       
        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
              
                product.RowVersion = GetProductVersion(product);
            }
            return product;
        }

        private byte[] GetProductVersion(Product product)
        {
            return BitConverter.GetBytes(DateTime.UtcNow.Ticks);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}