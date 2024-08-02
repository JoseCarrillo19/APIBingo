using APIBingo.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APIBingo.Application.DTOs
{
    public class ProductRequest : AuditableEntity
    {
        [Required(ErrorMessage = "IdCliente is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "IdCliente must be a positive number.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "Category cannot be longer than 100 characters.")]
        public string? Category { get; set; }
    }
}
