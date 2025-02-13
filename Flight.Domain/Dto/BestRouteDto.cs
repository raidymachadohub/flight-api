namespace Flight.Domain.Dto;

public class BestRouteDto
{
    public decimal TotalPrice { get; set; } = decimal.MaxValue;
    public List<RoutesDto> Routes { get; set; } = new();
}