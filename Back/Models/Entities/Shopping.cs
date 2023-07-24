namespace Back.Models.Entities
{
  public class Shopping
  {
    public Guid id { get; set; }
    public List<Product> products { get; set; }

    //fore keys
    public Guid UserId { get; set; }
    public User user { get; set; }
  }
}