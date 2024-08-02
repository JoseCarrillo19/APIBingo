namespace APIBingo.Domain.Entities
{
    public class ProductResponse
    {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public int ProductId { get; set; }
            public int ClientId { get; set; }
            public int StatusCode { get; set; }
    }
}
