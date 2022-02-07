using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string TempPhotoUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a short product description")]
        public string Description { get; set; }

        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter buying price")]
        public decimal? BuyingPrice { get; set; }

        [Required(ErrorMessage = "Please enter selling price")]
        public decimal? SellingPrice { get; set; }

        public string Slug { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
