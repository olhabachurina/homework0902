using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public decimal TotalPrice { get; set; } 
        public string Status { get; set; } 
        public string ShippingAddress { get; set; } 
        public List<OrderItem> OrderItems { get; set; } 

        
    }
}

