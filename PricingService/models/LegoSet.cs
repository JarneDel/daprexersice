namespace PricingService.models;

public class LegoSet
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string ModelNumber { get; set; }
    public int YearReleased { get; set; }
    public int PieceCount { get; set; }
    public string Theme { get; set; }
    public decimal Price { get; set; }
}