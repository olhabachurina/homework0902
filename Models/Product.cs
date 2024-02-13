using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework0902.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название товара обязательно для заполнения")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше нуля")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть неотрицательным числом")]
        public int Quantity { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } 
    }
}
