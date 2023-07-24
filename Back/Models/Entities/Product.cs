namespace Back.Models.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Characteristics { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool IsDelete { get; private set; } = false;

    public Product(string name, string brand, string model, string characteristics, decimal price, string image, bool isDelete)
    {
        Name = name;
        Brand = brand;
        Model = model;
        Characteristics = characteristics;
        Price = price;
        Image = image;
        IsDelete = isDelete;
    }
}