using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class Item
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Request contains ID value less than 1")]
        public int id { get; set; }
        [Required]
        public string itemName { get; set; }
        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between $0.01 and $1,000,000.00")]
        [RegularExpression(@"^\d+(.\d{1,2})?$", ErrorMessage = "Price must be limited to two decimal places")]
        public decimal cost { get; set; }
    }
}
