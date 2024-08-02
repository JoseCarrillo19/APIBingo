namespace APIBingo.Domain.Entities
{
    public class BingoNumber : AuditableEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Number { get; set; }
    }
}
