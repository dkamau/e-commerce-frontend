using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend.Models.Products
{
    public class ProductStockUpdate
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int? Quantity { get; set; }
        public decimal Total
        {
            get
            {
                if (Quantity != null && Price != null)
                    return (int)Quantity * (decimal)Price;
                else
                    return 0;
            }
        }
        public ProductStockUpdateType ProductStockUpdateType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public enum ProductStockUpdateType
    {
        Addition,
        Deduction
    }
}
