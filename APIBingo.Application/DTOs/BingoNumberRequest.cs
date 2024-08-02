using APIBingo.Domain.Entities;

namespace APIBingo.Application.DTOs
{
    public class BingoNumberRequest : AuditableEntity
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
    }
}
