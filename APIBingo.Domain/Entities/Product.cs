namespace APIBingo.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}
