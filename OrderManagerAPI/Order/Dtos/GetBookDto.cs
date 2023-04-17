namespace OrderManagerAPI.Order.Dtos;

public record GetBookDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int OrderId { get; set; }
}