namespace Back.Models.Entities
{
  public class Shopping
  {
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public ICollection<Product> Product { get; private set; } = new List<Product>();
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Shopping(Guid productId, Guid userId)
    {
        ProductId = productId;
        UserId = userId;
    }
}