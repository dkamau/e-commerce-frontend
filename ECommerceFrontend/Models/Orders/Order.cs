using ECommerceFrontend.Models.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend.Models.Orders
{
    public class Order
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public bool ShippingIsSameAsBilling { get; set; }
        public bool SaveInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}
