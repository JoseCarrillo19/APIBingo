namespace APIBingo.Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreationDate { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedMachine { get; set; }
    }
}
