namespace Back.Models.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Characteristics { get; set; }
    public decimal Price { get; set; }
    public string Images { get; set; }
}