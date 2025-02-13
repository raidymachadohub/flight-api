namespace Flight.Domain.Dto;

public class RoutesDto
{
    public Guid Id { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; }  = null!;
    public decimal Price { get; set; }
}