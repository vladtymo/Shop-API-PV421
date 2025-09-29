using DataAccess.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    // Data Transfer Objects
    public class CreateProductDto
    {
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
